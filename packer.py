import json
import os
import re
import shutil
import sys

# 默认的优先获取目录
PRIORITY_DIR = "Release"

def scan_directory_basenames(target_dir, ignore_dir_name=None):
    """递归扫描目录，返回一个字典：{basename: [full_paths]}

    增加 ignore_dir_name 参数，用于在扫描当前目录时排除 Release 文件夹
    """
    mapping = {}
    if not os.path.exists(target_dir) or not os.path.isdir(target_dir):
        print(f"目标文件不存在: {target_dir}")
        return mapping

    for root, dirs, files in os.walk(target_dir):
        # 如果设置了忽略目录，且当前目录名符合，则跳过该子目录的扫描
        if ignore_dir_name and ignore_dir_name in dirs:
            dirs.remove(ignore_dir_name)  # 动态修改 dirs 可以阻止 os.walk 进入该目录

        for file in files:
            full_path = os.path.abspath(os.path.join(root, file))
            if file not in mapping:
                mapping[file] = []
            mapping[file].append(full_path)
    return mapping


def fix_git_bash_path(path):
    """修复 Git Bash 路径导致的盘符错位问题"""
    match = re.match(r"^/([a-zA-Z])(/|$)(.*)", path)
    if match:
        drive = match.group(1).upper()
        rest = match.group(3)
        fixed_path = f"{drive}:/{rest}"
        return os.path.abspath(fixed_path)
    return os.path.abspath(path)


def generate_mappings(src_dir):
    """只需传入参考发布包目录，纯粹记录其内部的相对目录结构"""
    src_dir = fix_git_bash_path(src_dir)
    src_dir_abs = os.path.abspath(src_dir)

    print(f"--- 开始扫描参考发布包目录: {src_dir_abs} ---")
    src_map = scan_directory_basenames(src_dir_abs)

    # 结构设计：{ "filename.dll": ["SubFolder/filename.dll", "AnotherFolder/filename.dll"] }
    # 这样在还原时，可以通过 basename 瞬间秒查到它应该去往哪些相对路径
    structure_mapping = {}

    for basename, src_paths in src_map.items():
        structure_mapping[basename] = []
        for sp in src_paths:
            # 计算文件相对于发布包根目录的相对路径
            rel_path_from_src = os.path.relpath(sp, src_dir_abs)
            # 统一转换成正斜杠，防止跨平台输出 JSON 格式不一致
            rel_path_formatted = rel_path_from_src.replace("\\", "/")
            if rel_path_formatted not in structure_mapping[basename]:
                structure_mapping[basename].append(rel_path_formatted)

    # 保存映射文件
    map_filename = "release_structure.json"
    with open(map_filename, "w", encoding="utf-8") as f:
        json.dump(structure_mapping, f, indent=4, ensure_ascii=False)

    print(f"\n[成功] 参考发布包结构已生成（仅保留相对路径）")
    print(f"结构映射文件: {os.path.abspath(map_filename)}")


def reconstruct_from_reverse_map():
    """根据发布包结构，在当前目录下搜寻文件并重构到 Release 目录中"""
    map_filename = "release_structure.json"
    if not os.path.exists(map_filename):
        print(f"错误: 未找到映射文件 '{map_filename}'，请先指定参考发布包生成映射。")
        return

    with open(map_filename, "r", encoding="utf-8") as f:
        structure_mapping = json.load(f)

    current_dir = os.getcwd()
    output_release_dir = os.path.join(current_dir, "..", "Release")

    print(f"--- 开始构建重构任务 ---")
    print(f"打包输出目录 (Release): {output_release_dir}")

    current_files_map = scan_directory_basenames(current_dir)

    # 如果输出太长可以视情况隐去
    # print(f'{current_files_map}')

    success_count = 0
    missing_count = 0
    copy_failed_count = 0
    failed_list = [] 

    # 遍历期望构建的文件名和它们对应的相对路径
    for basename, rel_paths in structure_mapping.items():
        if basename not in current_files_map or not current_files_map[basename]:
            for rel_path in rel_paths:
                print(f"[缺失] 本地未找到原始文件: {basename}，无法填充路径: {rel_path}")
                missing_count += 1
            continue

        # 找到了可用文件
        available_paths = current_files_map[basename]
        chosen_source_file = available_paths[0]

        # 把这个文件复制到所有它应该去的相对路径里
        for rel_path in rel_paths:
            final_target_path = os.path.abspath(
                os.path.join(output_release_dir, rel_path)
            )

            # 创建多级子目录
            os.makedirs(os.path.dirname(final_target_path), exist_ok=True)

            try:
                print(f"copy {chosen_source_file} -> {final_target_path}")
                shutil.copy2(chosen_source_file, final_target_path)
                success_count += 1
            except Exception as e:
                print(f"[失败] 无法拷贝 {basename} -> {final_target_path}. 错误: {e}")
                copy_failed_count += 1
                failed_list.append(basename)

    print(f"\n--- 重构打包结束 ---")
    print(f"成功填充并复制的文件数: {success_count}")
    print(f"缺失的文件数: {missing_count}")
    print(f"复制失败数量: {copy_failed_count}, 列表: {failed_list}")


if __name__ == "__main__":
    args = sys.argv[1:]

    if len(args) == 0:
        print("开始根据结构还原打包...")
        reconstruct_from_reverse_map()
    elif len(args) == 1:
        # 【修改】：现在只需要 1 个参数即可
        generate_mappings(args[0])
    else:
        print("使用方法说明:")
        print(f"  1. 生成映射: python {sys.argv[0]} <参考发布包目录>")
        print(f"  2. 还原重构: python {sys.argv[0]}")
        sys.exit(1)