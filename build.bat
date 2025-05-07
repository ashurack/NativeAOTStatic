@echo off
setlocal

REM Build the docker image
docker build -t native-aot-static -f aot-static.Dockerfile .

REM Run build-static.sh inside the container
docker run --rm -it ^
  -v "%cd%:/app" ^
  -w /app ^
  native-aot-static ^
  sh build-static.sh