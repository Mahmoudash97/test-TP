from flask import Flask, request, jsonify
from logic.pose_analysis import analyze_pose

from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/process-video', methods=['POST'])
def process_video():
    data = request.json
    video_url = data.get('VideoUrl')

    if not video_url:
        return jsonify({"error": "No video URL provided"}), 400

    # Mock AI processing logic
    result = {
        "score": 85.5,
        "feedback": "Good start, but improve arm alignment."
    }
    return jsonify(result), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8000)

