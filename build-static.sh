#!/bin/sh

dotnet publish -c Release --force /bl:publish.binlog
MSBuildExtractLinkNative/bin/Release/net9.0/publish/MSBuildExtractLinkNative /app/publish.binlog | jq -c '.[]' | while read -r row; do
  dir=$(echo "$row" | jq -r '.project_path')
  cmd=$(echo "$row" | jq -r '.static_build_command')
  output=$(echo "$row" | jq -r '.output_file')
  echo "Recompiling $dir"
  echo "Command: $cmd"
  echo "Output: $output"
  (cd "$dir" && eval "$cmd" && eval "strip $output")
done