
void mainImage(out vec4 fragColor)
{
    vec2 p = fragCoord * vec2(2.0);
    
    vec2  r;
    float red = p.x;
    float blue = p.y;
    float green = abs(red + blue / 2.0);
    vec3 destColor = vec3(red, green, blue);
    float f = 0.0;
    for(float i = 0.0; i < 10.0; i++)
    {
        float s = sin(iTime * 4.0 + i * 0.628318) * 0.5;
        float c = cos(iTime * 9.0 + i * 0.628318) * 0.5;
        f += 0.005 / abs(length(p + vec2(c, s)) - abs(sin(iTime) * 0.8));
    }
    fragColor = vec4(destColor, f > 0.1 ? f : 0.0);
}