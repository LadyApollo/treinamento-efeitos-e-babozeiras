uniform extern texture ScreenTexture;
uniform extern float filterValue;


sampler ScreenFilter = sampler_state
{
	Texture = <ScreenTexture>;
};

texture backgroundTexture;

sampler ScreenB = sampler_state
{
	Texture = <backgroundTexture>;
};





float4 PixelShaderFunction(float2 curCoord : TEXCOORD0) : COLOR
{
	float4 color = tex2D(ScreenB, curCoord);
	float4 colorFilter = tex2D(ScreenFilter, curCoord);

	if(filterValue > colorFilter.r)
		discard;

	return color;
}

technique
{
	pass P0
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}