float4x4 World;
float4x4 View;
float4x4 Projection;

extern bool BlurActived;

extern texture FinalTexture;
const float NUM_PASSES = 8;


// TODO: add effect parameters here.

sampler TextureSampler = sampler_state
{
	texture = FinalTexture;
	magFilter = POINT;
	minFilter = POINT;
	mipFilter = POINT;
};

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

float4 PSBlur (float2 TexCoord : TEXCOORD0 ) : COLOR0
{
	float4 Color = tex2D(TextureSampler, TexCoord);


	if(BlurActived == true)
	{
 
		for( int i = 0; i < NUM_PASSES; i++ )
		{
			Color += tex2D(TextureSampler, TexCoord + BlurPasses[i]);
		}
		Color /= (NUM_PASSES + 1);
 
	}
}

technique PostEffect
{
	pass Blur
	{
		
		PixelShader = compile ps_2_0 PSBlur();
	}
}

struct VertexShaderInput
{
    float4 Position : POSITION0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.

    return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
