extern texture FinalTexture;

sampler TextureSampler = sampler_state
{
	texture = <FinalTexture>;
	magFilter = POINT; minFilter = POINT; mipFilter = POINT;
};

const float NUM_PASSES = 8;

const float2 BlurPasses[8] =
{
	float2(0.003, 0),
	float2(0.004, 0),
	float2(0, 0.003),
	float2(0, 0.004),
	
	float2(-0.0033, 0),
	float2(-0.0043, 0),
	float2(0, -0.0033),
	float2(0, -0.0043) 
};

extern bool BlurActived;

float4 PSBlur ( float2 TexCoord : TEXCOORD0 ) : COLOR0
{
	float4 Color = tex2D(TextureSampler, TexCoord);
	
	if(BlurActived == true)
	{
	
		for( int i = 0; i < NUM_PASSES; i++ )
		{
			Color += tex2D(TextureSampler, TexCoord + BlurPasses[i]);
		}
	
		Color /= NUM_PASSES + 1;
		
	}
	
	return Color;
}

technique PostEffect
{

	pass Blur
	{
		PixelShader = compile ps_2_0 PSBlur();
	}

}