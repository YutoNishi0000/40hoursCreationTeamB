//自然対数を底として、光が減衰するときの媒介変数を取得するための関数
float GetFogParameter(float3 objectPos, float3 cameraPos, float density)
{
	//カメラからオブジェクトに向かっているベクトルを取得
	float3 camToObjVec = objectPos - cameraPos;
	//求めたベクトルの長さを取得
	float distance = length(camToObjVec);
	// もとはカラーの減衰率を累乗するような計算を簡略化している
	// 具体的には光の減衰するときの媒介変数を求めるために
	// float lost = 1.0 - pow((1.0 - density), distance);という式を立てる（ここでは失った光の量を求めている）
	// ここではどれぐらい光が失われるかを求めたいため
	// y = pow((1.0 - density), distance)に着目し、両辺の対数をとることで
	// log(y) = log((1.0 - density)^distance)
	// log(y) = distance * log(1.0 - density)
	// よって
	// y = exp(distance * log(1.0 - density))
	// となる
	// ここで、log(1-x)をテイラー展開して、一次で打ち切ることにより
	// log(1-x) = -xと近似することができる
	// したがって
	// float3 parameter = exp(distance * -density)となる
	float parameter = exp(distance * -density);
	return parameter;
}

float GetFogHeightParameter(float3 objectPos, float3 cameraPos, float fogDensity, float fogEndHeight)
{
	//前提知識として、カメラとオブジェクトの位置関係から、相似比を出して、光が霧の中をどれだけ進むかという距離を算出する
	// 
	// 公式：光が霧の中を進む距離 = カメラとオブジェクト間の距離 * (霧の高さ - オブジェクトのy座標) / カメラとオブジェクト間のベクトルのy成分
	// 
	// この霧の中を進む距離を求める
	float3 camToObjVec = objectPos - cameraPos;	 //カメラからオブジェクトのベクトル
	float t;                                     //相似比率
	//オブジェクトが霧の外にあるのであれば
	if (objectPos.y > fogEndHeight)
	{
		//カメラが霧の外にあるのであれば
		if (cameraPos.y > fogEndHeight)
		{
			t = 0;     //求める距離全てが霧の中を進んでいない
		}
		//カメラが霧中にあるのであれば
		else
		{
			t = (cameraPos.y - fogEndHeight) / camToObjVec.y;
		}
	}
	//オブジェクトが霧の中にあるのであれば
	else
	{
		//カメラが霧の外にあるのであれば
		if (cameraPos.y > fogEndHeight)
		{
			t = (fogEndHeight - objectPos.y) / camToObjVec.y;
		}
		//カメラが霧中にあるのであれば
		else
		{
			t = 1;     //求める距離全てが霧の中を進んでいる
		}
	}

	float fogDistance = length(camToObjVec) * t;         //光が霧の中を進む距離
	float parameter = exp(-fogDistance * fogDensity);    //光の減衰率を表した媒介変数
	return parameter;
}

//オブジェクト一つ一つに対する光の減衰率を表す媒介変数を取得する関数（多分この計算論理的ではないから後々修正する予定）
float GetForHeightFogParameter(float3 objectPos, float3 cameraPos, float densityY0, float densityAttenuation)
{
	// 高さが高くなるごとに霧を薄くする
	// 高さに応じた霧の濃さによる光の減衰量を算出すればよい　
	// 高さをy、地上での霧の濃さをd0、定数をk（実数）とし、カメラの所で0、物の所で100になるような変数をsとする
	// sで微分すると、微分されたcの値が求まる->積分すればオブジェクトの光の減衰率がわかる->ds分大きくなるとdc分光が減衰されるイメージ
	// dc / ds = -c * d0 * exp(-k * y);
	// ここでyが邪魔なのでyをsで表す
	// y = y0 + (v.y / |v| * s);
	// よってsが|v|になれば、分子分母が消えてv.yが残り、これをy0に足せばカメラの高さになる（相似比から求める）
	// dc / ds = -c * d0 * exp(-k * (y0 + (v.y / |v| * s)))
	// ここで A = d0 * exp(-k * y0)、B = -k * v.y / |v| とする
	// そして
	// dc / ds = -c * A * exp(B * s)となる
	// ここで、cを左辺に、dsを右辺に移項
	// dc / c = -A * exp(B * s) * ds
	// これを積分する
	// log|c| = -A / B * exp(B * s) + C (Cは積分定数)
	// ここで、cを求めたいため、定積分（指定した範囲）(0 ～ |v|)を求める
	// |c| = exp(-A/B * (exp(-k * v.y) - 1))
	// c = exp(d0 * |v| / (k * v.y) * exp(-k * y0) * (exp(-k * v.y) - 1))
	// これはまとめると指定した高さになるまで積分（ループ）して減衰されたカラー情報を返すことになる
	float3 camToObjVec = cameraPos - objectPos;      //カメラとオブジェクト間のベクトル
	float l = length(camToObjVec);                             //カメラとオブジェクト間の距離
	float ret;                                        //霧の媒介変数
	float tmp = l * densityY0 * exp(-densityAttenuation * objectPos.y);   //上記で言うA（定数）
	if (camToObjVec.y == 0.0) // 単純な均一フォグ
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