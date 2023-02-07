using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Scenario
{
    //day1
    static public readonly List<MessageData> meslist_day1 = new List<MessageData>()
    {
        // �J�n��
        new MessageData{ massage = "����...", color = Color.magenta },
        new MessageData{ massage = "����<color=#ffff00>�g���̐l�h</color>�������Ȃ�����c", color = Color.magenta },
        new MessageData{ massage = "�ł��C�Â���Ȃ��悤�ɂ��Ȃ��Ɓc", color = Color.magenta },
        new MessageData{ massage = "�����Ď��́c", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day2
    static public readonly List<MessageData> meslist_day2 = new List<MessageData>()
    {
        // �J�n��
        new MessageData{ massage = "�������܂��g���̐l�h��������Ƃ��납��n�߂Ȃ���c", color = Color.magenta },
        new MessageData{ massage = "���̐l�͂ǂ��Ɍ������Ă�̂��ȁc�H", color = Color.magenta },
        new MessageData{ massage = "�u���X����o�Ă����Ƃ���v�����ʂ����Ⴈ���I", color = Color.magenta },
        new MessageData{ massage = "�B��Ȃ���ʐ^���B���ꏊ���邩�ȁc", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day3
    static public readonly List<MessageData> meslist_day3 = new List<MessageData>()
    {
        // �J�n��
        new MessageData{ massage = "�͂��c������3���ڂ��c", color = Color.magenta },
        new MessageData{ massage = "�����͉������悤���ȁc", color = Color.magenta },
        new MessageData{ massage = "�Ƃ肠���������U�􂵂g���̐l�h�������悤", color = Color.magenta },
        new MessageData{ massage = "�܂�����������܂�����ˁc", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // �n���J�`�擾��
        new MessageData{ massage = "�n���J�`���B�N�����Ƃ���������̂��ȁH", color = Color.magenta },
        new MessageData{ massage = "�c��������������Ɂg���̐l�h�ɘb��������Ȃ�����", color = Color.magenta },
        new MessageData{ massage = "�{���̗��Ƃ��傳��ɂ͈������ǁc", color = Color.magenta },
        new MessageData{ massage = "��i������Ă�ɂ͂Ȃ���ˁI", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // �n���J�`�E���Ȃ�������
        new MessageData{ massage = "���[���B���������ł��Ȃ������ȁc", color = Color.magenta },
        new MessageData{ massage = "�g���̐l�h�Ƃ͈ꐶ�b���Ȃ��̂��ȁc", color = Color.magenta },
        new MessageData{ massage = "��������������΂����񂾂���", color = Color.magenta },
        new MessageData{ massage = "�������������c", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day4
    static public readonly List<MessageData> meslist_day4 = new List<MessageData>()
    {
        // �J�n��
        new MessageData{ massage = "�n���J�`�́c�悵�B�����Ǝ����Ă��Ă�", color = Color.magenta },
        new MessageData{ massage = "�u���ꗎ�Ƃ��܂������H���Ȃ��̂��Ǝv���āc�v", color = Color.magenta },
        new MessageData{ massage = "�\�s���K�͂���Ȋ����ł悵��", color = Color.magenta },
        new MessageData{ massage = "���������g���̐l�h�ɘb��������񂾁c�I", color = Color.magenta },
        new MessageData{ massage = "�ł����ʂ���͒p������������u��납��v�b�������悤�c", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // ���Ԑ؂�
        new MessageData{ massage = "����ς肱��ȉ�������g���Ęb�������悤�Ȃ�āc", color = Color.magenta },
        new MessageData{ massage = "�c����A�����b�������邱�Ƃ���ł��Ȃ������ւ̌����󂾂�c", color = Color.magenta },
        new MessageData{ massage = "�_�����ƂŖ����b���������Ⴈ�����ȁc", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        //�n���J�`�C�x���g
        new MessageData{ massage = "���A���́I", color = Color.magenta },
        new MessageData{ massage = "�́A�͂��H", color = Color.cyan },
        new MessageData{ massage = "���ꗎ�Ƃ��܂������H���Ȃ��̂��Ǝv���āE�E�E", color = Color.magenta },
        new MessageData{ massage = "���E�E���A�Ⴂ�܂�", color = Color.cyan },
        new MessageData{ massage = "�����ł����c���̕��Ŏ����傳��T���Ă݂܂��ˁI", color = Color.magenta },
        new MessageData{ massage = "�����ƍ����Ă��邾�낤���c", color = Color.magenta },
        new MessageData{ massage = "�����ł��ˁc�����匩����Ƃ����ł��ˁI", color = Color.cyan },
        new MessageData{ massage = "�S�D�������ɏE���Ă��炦���悤�ŗǂ������ł�", color = Color.cyan },
        new MessageData{ massage = "�ł́A���������傪�����邱�Ƃ��F���Ă܂��I", color = Color.cyan },
        new MessageData{ massage = "#", color = Color.cyan },

    };
    // day5
    static public readonly List<MessageData> meslist_day5 = new List<MessageData>()
    {
        // �J�n��
        new MessageData{ massage = "���������g���̐l�h�ɘb��������񂾁c�I", color = Color.magenta },
        new MessageData{ massage = "�����|�����ǂ����Ƒ��v����ˁc", color = Color.magenta },
        new MessageData{ massage = "�c�@�c", color = Color.magenta },
        new MessageData{ massage = "�悵�A�s����", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        
        
    };
    // day5 �n���J�`��n���Ă����ꍇ
    static public readonly List<MessageData> meslist_GameClear = new List<MessageData>()
    {
        new MessageData{ massage = "���A���́I", color = Color.magenta },
        new MessageData{ massage = "�́A�͂��c", color = Color.cyan },
        new MessageData{ massage = "���I���Ȃ��͍���́I", color = Color.cyan },
        new MessageData{ massage = "�o���Ă���Ăėǂ������ł��I", color = Color.magenta },
        new MessageData{ massage = "�n���J�`�̎�����͌�����܂������H", color = Color.cyan },
        new MessageData{ massage = "���[�A�́A�͂��I������܂����I", color = Color.magenta },
        new MessageData{ massage = "����͗ǂ������ł�", color = Color.cyan },
        new MessageData{ massage = "�c�@�c", color = Color.magenta },
        new MessageData{ massage = "���́[�B���̃L�[�z���_�[���āc", color = Color.magenta },
        new MessageData{ massage = "����ł����H���̑O�w�O�ŗ����Ă�̂��E������ł���ˁ`", color = Color.cyan },
        new MessageData{ massage = "���A������������ł����c���͂��ꐶ�Y�����E��1�����̌����A�i�Łc", color = Color.magenta },
        new MessageData{ massage = "���A�����Ȃ�ł����I�H", color = Color.cyan },
        new MessageData{ massage = "�͂��c����������u�~���N���_�u���o�C�Z�b�v�X�L�[�z���_�[�v�Ȃ̂Łc", color = Color.magenta },
        new MessageData{ massage = "���A���Ȃ�����������̂�������ł��ˁc", color = Color.cyan },
        new MessageData{ massage = "����̕����Ƃ͎v���܂���ł����I�����x�����Ăт�����ł��I", color = Color.cyan },
        new MessageData{ massage = "�����Ȃ�ł��c�����悯��ΕԂ��Ă�����Ă�낵���ł����H", color = Color.magenta },
        new MessageData{ massage = "���A�͂��c���v�ł��B�_�T����Łc", color = Color.cyan },
        new MessageData{ massage = "���c�H", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day5 �n���J�`��n���Ă��Ȃ��ꍇ
    static public readonly List<MessageData> meslist_GameOver = new List<MessageData>()
    {
        new MessageData{ massage = "���A���́I", color = Color.magenta },
        new MessageData{ massage = "�́A�͂��E�E�E", color = Color.magenta },
        new MessageData{ massage = "��A�킽���E�E�E���́E�E�E", color = Color.magenta },
        new MessageData{ massage = "���[�A�����l�Ⴂ���Ǝv���܂��̂ŁE�E�E", color = Color.magenta },
        new MessageData{ massage = "���A����A���́E�E�E", color = Color.magenta },
        new MessageData{ massage = "���ꂶ��A���炵�܂�", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
}
