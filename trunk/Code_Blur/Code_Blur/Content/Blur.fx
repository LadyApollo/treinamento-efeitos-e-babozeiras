extern texture FinalTexture;
extern int textureHight;
extern int textureWidth;


sampler TextureSampler = sampler_state
{
	texture = <FinalTexture>;
	magFilter = POINT;
	minFilter = POINT;
	mipFilter = POINT;
};



float4 PSBlur ( float2 TexCoord : TEXCOORD0 ) : COLOR0
{/*
	float4 Color = tex2D(TextureSampler, TexCoord);
	Color -= tex2D(TextureSampler , TexCoord.xy-0.003)*2.7f; 
	Color += tex2D( TextureSampler , TexCoord.xy+0.003)*2.7f; 
	Color.rgb = (Color.r+Color.g+Color.b)/3.0f;*/
		
	/*float4 Color = tex2D(TextureSampler, TexCoord);
	if(Color.r + Color.b + Color.g > 0.3)
	{
		Color.r = Color.b = Color.g = 1;
	}
	else
	{
		Color.r = Color.b = Color.g = 0;
	}
	
*/
	float pixelWidth = 1.0f / textureWidth;
	float pixelHeight = 1.0f / textureHight;
	
	//return tex2D(TextureSampler, float2(0, pixelHeight * 15));
	
	if (TexCoord.y * textureWidth == 5)
	{
		return float4(1, 1, 1, 1);
	}
	else
	{
		return tex2D(TextureSampler, TexCoord);
	}
			
	//float4 Color = tex2D(TextureSampler, TexCoord + float2(0.00125f * 10, 0));
	//return Color;
}

technique PostEffect
{

	pass Blur
	{
		PixelShader = compile ps_2_0 PSBlur();
	}

}