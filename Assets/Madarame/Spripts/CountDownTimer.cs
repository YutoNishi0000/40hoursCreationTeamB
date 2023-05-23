using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float _totalTime;    // �������Ԃ̍��v
    private float _oldSeconds;   // �O��Update���̕b��
    private Text _timerText;
    [SerializeField] private int _minute;  // �������ԁi���j
    [SerializeField] private float _seconds; // �������ԁi�b�j

    void Start()
    {
		_totalTime = _minute * 60 + _seconds;
		_oldSeconds = 0f;
		_timerText = GetComponentInChildren<Text>();
    }

    void Update()
    {
		//�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
		if (_totalTime <= 0f)
		{
			SEManager.Instance.PlayTimeLimit(2f);
			GameManager.Instance.SetGameOver(true);
			return;
		}
		//�@��U�g�[�^���̐������Ԃ��v���G
		_totalTime = _minute * 60 + _seconds;
		_totalTime -= Time.deltaTime;

		//�@�Đݒ�
		_minute = (int)_totalTime / 60;
		_seconds = _totalTime - _minute * 60;

		//�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
		if ((int)_seconds != (int)_oldSeconds)
		{
			_timerText.text = _minute.ToString("00") + ":" + ((int)_seconds).ToString("00");
		}
		_oldSeconds = _seconds;
		//�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
		if (_totalTime <= 0f)
		{
			Debug.Log("�������ԏI��");
		}
	}
}
