
from flask import Flask, request, abort, send_file
import os
import gzip
import io
from threading import Thread
app = Flask(__name__)
content_dir = os.path.join(os.path.dirname(__file__), '..', 'continut')

@app.route('/<path:path>')
def hello_world(path):
    file_path = os.path.join(content_dir, path)
    if not os.path.exists(file_path):
        abort(404)

    content_type = get_content_type(path)

# read the contents of the file in the buffer
    with open(file_path, 'rb') as f:
        content = f.read()


# compress the content in gzip format
    compressed_content = gzip.compress(content)


# set the Content-Encoding header in the server's response to gzip
    response = send_file(io.BytesIO(compressed_content), mimetype=content_type)
    response.headers['Content-Encoding'] = 'gzip'
    response.headers['Content-Length'] = len(compressed_content)

    return response

def get_content_type(file_path):
    extension = os.path.splitext(file_path)[1].lower()
    if extension == '.html':
        return 'text/html'
    elif extension == '.css':
        return 'text/css'
    elif extension == '.js':
        return 'application/javascript'
    elif extension == '.png':
        return 'image/png'
    elif extension == '.jpg' or extension == '.jpeg':
        return 'image/jpeg'
    elif extension == '.gif':
        return 'image/gif'
    elif extension == '.ico':
        return 'image/x-icon'
    else:
        return 'application/octet-stream'

def run_server():
    app.run(port=5678)

if __name__ == '__main__':
    server_thread = Thread(target=run_server)
    server_thread.start()