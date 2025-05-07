jq -c '.[]' | while read -r row; do
  dir=$(echo "$row" | jq -r '.project_path')
  cmd=$(echo "$row" | jq -r '.static_build_command')
  echo "Recompiling $dir"
  echo "Command: $cmd"
  (cd "$dir" && eval "$cmd")
done
