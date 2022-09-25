class FramerateCounter {
    private float _duration = 0;
    private int _frames = 0;
    private float _sampleDuration = 1f;

    private const string _textFormat = "FPS: {0}";

    private float _prevValue = 0f;

    public FramerateCounter(float sampleDuration) {
        _sampleDuration = sampleDuration;
    }

    public void Update(float deltaTime) {
        _frames += 1;
        _duration += deltaTime;

        if (_duration >= _sampleDuration) {
            _prevValue = _frames / _duration;
            _frames = 0;
            _duration = 0;
        }
    }

    public override string ToString() {
        return string.Format(_textFormat, _prevValue.ToString("0"));
    }
}