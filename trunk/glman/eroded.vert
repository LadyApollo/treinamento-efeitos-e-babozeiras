const vec3 LIGHT = vec3(10, 10, 10);

flat out float vLightIntensity;

void main()
{
	vec3 transNorm = normalize(vec3(gl_NormalMatrix * gl_Normal));
	vec3 ECposition = vec3(gl_ModelViewMatrix * gl_Vertex);

	vLightIntensity = dot(normalize(LIGHT - ECposition), transNorm);
		

	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
}
