#!/bin/sh
set -e

# Build the Docker image
docker build -t native-aot-static -f aot-static.Dockerfile .

# Run build-static.sh inside the container
docker run --rm -it \
  -v "$(pwd):/app" \
  -w /app \
  native-aot-static \
  sh build-static.sh