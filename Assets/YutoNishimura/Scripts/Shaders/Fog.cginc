//���R�ΐ����Ƃ��āA������������Ƃ��̔}��ϐ����擾���邽�߂̊֐�
float GetFogParameter(float3 objectPos, float3 cameraPos, float density)
{
	//�J��������I�u�W�F�N�g�Ɍ������Ă���x�N�g�����擾
	float3 camToObjVec = objectPos - cameraPos;
	//���߂��x�N�g���̒������擾
	float distance = length(camToObjVec);
	// ���Ƃ̓J���[�̌�������ݏ悷��悤�Ȍv�Z���ȗ������Ă���
	// ��̓I�ɂ͌��̌�������Ƃ��̔}��ϐ������߂邽�߂�
	// float lost = 1.0 - pow((1.0 - density), distance);�Ƃ������𗧂Ă�i�����ł͎��������̗ʂ����߂Ă���j
	// �����ł͂ǂꂮ�炢���������邩�����߂�������
	// y = pow((1.0 - density), distance)�ɒ��ڂ��A���ӂ̑ΐ����Ƃ邱�Ƃ�
	// log(y) = log((1.0 - density)^distance)
	// log(y) = distance * log(1.0 - density)
	// �����
	// y = exp(distance * log(1.0 - density))
	// �ƂȂ�
	// �����ŁAlog(1-x)���e�C���[�W�J���āA�ꎟ�őł��؂邱�Ƃɂ��
	// log(1-x) = -x�Ƌߎ����邱�Ƃ��ł���
	// ����������
	// float3 parameter = exp(distance * -density)�ƂȂ�
	float parameter = exp(distance * -density);
	return parameter;
}

float GetFogHeightParameter(float3 objectPos, float3 cameraPos, float fogDensity, float fogEndHeight)
{
	//�O��m���Ƃ��āA�J�����ƃI�u�W�F�N�g�̈ʒu�֌W����A��������o���āA�������̒����ǂꂾ���i�ނ��Ƃ����������Z�o����
	// 
	// �����F�������̒���i�ދ��� = �J�����ƃI�u�W�F�N�g�Ԃ̋��� * (���̍��� - �I�u�W�F�N�g��y���W) / �J�����ƃI�u�W�F�N�g�Ԃ̃x�N�g����y����
	// 
	// ���̖��̒���i�ދ��������߂�
	float3 camToObjVec = objectPos - cameraPos;	 //�J��������I�u�W�F�N�g�̃x�N�g��
	float t;                                     //�����䗦
	//�I�u�W�F�N�g�����̊O�ɂ���̂ł����
	if (objectPos.y > fogEndHeight)
	{
		//�J���������̊O�ɂ���̂ł����
		if (cameraPos.y > fogEndHeight)
		{
			t = 0;     //���߂鋗���S�Ă����̒���i��ł��Ȃ�
		}
		//�J�����������ɂ���̂ł����
		else
		{
			t = (cameraPos.y - fogEndHeight) / camToObjVec.y;
		}
	}
	//�I�u�W�F�N�g�����̒��ɂ���̂ł����
	else
	{
		//�J���������̊O�ɂ���̂ł����
		if (cameraPos.y > fogEndHeight)
		{
			t = (fogEndHeight - objectPos.y) / camToObjVec.y;
		}
		//�J�����������ɂ���̂ł����
		else
		{
			t = 1;     //���߂鋗���S�Ă����̒���i��ł���
		}
	}

	float fogDistance = length(camToObjVec) * t;         //�������̒���i�ދ���
	float parameter = exp(-fogDistance * fogDensity);    //���̌�������\�����}��ϐ�
	return parameter;
}

//�I�u�W�F�N�g���ɑ΂�����̌�������\���}��ϐ����擾����֐��i�������̌v�Z�_���I�ł͂Ȃ������X�C������\��j
float GetForHeightFogParameter(float3 objectPos, float3 cameraPos, float densityY0, float densityAttenuation)
{
	// �����������Ȃ邲�Ƃɖ��𔖂�����
	// �����ɉ��������̔Z���ɂ����̌����ʂ��Z�o����΂悢�@
	// ������y�A�n��ł̖��̔Z����d0�A�萔��k�i�����j�Ƃ��A�J�����̏���0�A���̏���100�ɂȂ�悤�ȕϐ���s�Ƃ���
	// s�Ŕ�������ƁA�������ꂽc�̒l�����܂�->�ϕ�����΃I�u�W�F�N�g�̌��̌��������킩��->ds���傫���Ȃ��dc���������������C���[�W
	// dc / ds = -c * d0 * exp(-k * y);
	// ������y���ז��Ȃ̂�y��s�ŕ\��
	// y = y0 + (v.y / |v| * s);
	// �����s��|v|�ɂȂ�΁A���q���ꂪ������v.y���c��A�����y0�ɑ����΃J�����̍����ɂȂ�i�����䂩�狁�߂�j
	// dc / ds = -c * d0 * exp(-k * (y0 + (v.y / |v| * s)))
	// ������ A = d0 * exp(-k * y0)�AB = -k * v.y / |v| �Ƃ���
	// ������
	// dc / ds = -c * A * exp(B * s)�ƂȂ�
	// �����ŁAc�����ӂɁAds���E�ӂɈڍ�
	// dc / c = -A * exp(B * s) * ds
	// �����ϕ�����
	// log|c| = -A / B * exp(B * s) + C (C�͐ϕ��萔)
	// �����ŁAc�����߂������߁A��ϕ��i�w�肵���͈́j(0 �` |v|)�����߂�
	// |c| = exp(-A/B * (exp(-k * v.y) - 1))
	// c = exp(d0 * |v| / (k * v.y) * exp(-k * y0) * (exp(-k * v.y) - 1))
	// ����͂܂Ƃ߂�Ǝw�肵�������ɂȂ�܂Őϕ��i���[�v�j���Č������ꂽ�J���[����Ԃ����ƂɂȂ�
	float3 camToObjVec = cameraPos - objectPos;      //�J�����ƃI�u�W�F�N�g�Ԃ̃x�N�g��
	float l = length(camToObjVec);                             //�J�����ƃI�u�W�F�N�g�Ԃ̋���
	float ret;                                        //���̔}��ϐ�
	float tmp = l * densityY0 * exp(-densityAttenuation * objectPos.y);   //��L�Ō���A�i�萔�j
	if (camToObjVec.y == 0.0) // �P���ȋψ�t�H�O
	{
		ret = exp(-tmp);
	}
	else
	{
		float kvy = densityAttenuation * camToObjVec.y;   //(k * v.y)
		ret = exp(tmp / kvy * (exp(-kvy) - 1.0));         //c = exp(d0 * |v| / (k * v.y) * exp(-k * y0) * (exp(-k * v.y) - 1))
	}
	return ret;
}