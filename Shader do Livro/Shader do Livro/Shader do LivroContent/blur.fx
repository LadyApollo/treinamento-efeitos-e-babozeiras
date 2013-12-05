uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};

texture backgroundTexture;

sampler ScreenB = sampler_state
{
	Texture = <backgroundTexture>;
};

float2 center;



float4 PixelShaderFunction(float2 curCoord : TEXCOORD0) : COLOR
{
	
	float maxDistSQR = 0.7071f; //sqrt(0.5f)
	
	float2 diff = abs(curCoord - center);
	float distSQR = length(diff);
	
	float blurAmount = (distSQR / maxDistSQR);
	


	
	float4 color = tex2D(ScreenS, curCoord);
	float4 colorB = tex2D(ScreenB, curCoord);
	color[0] = lerp(color[0], colorB[0], blurAmount);
	color[1] = lerp(color[1], colorB[1], blurAmount);
	color[2] = lerp(color[2], colorB[2], blurAmount);
	return color;
}

technique
{
	pass P0
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}