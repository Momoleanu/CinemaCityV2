@echo off
echo Lansare server...
set FLASK_APP=server_web.py

python -m flask run --port=5678