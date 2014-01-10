#version 330 compatibility


flat in float vLightIntensity;


out vec4 fFragColor;

void main( )
{
	vec3 tempColor = vec3(1, 0.5, 0.4) * vLightIntensity;

	fFragColor = vec4(tempColor , 1);
}
