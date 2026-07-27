/*{
  "DESCRIPTION": "A rotating kaleidoscope fold — mirrored wedges spin continuously around center, fully automatic.",
  "CATEGORIES": ["Guillotine", "Stylize"],
  "INPUTS": [
    { "NAME": "inputImage", "TYPE": "image" },
    { "NAME": "segments", "TYPE": "float", "DEFAULT": 6.0, "MIN": 3.0, "MAX": 12.0 },
    { "NAME": "speed", "TYPE": "float", "DEFAULT": 0.5, "MIN": 0.0, "MAX": 2.0 }
  ]
}*/

void main() {
  vec2 uv = isf_FragNormCoord - vec2(0.5);
  float radius = length(uv);
  float angle = atan(uv.y, uv.x);

  // Fold the angle into one mirrored wedge (a triangle wave over 2x the wedge width), then spin
  // the whole fold continuously with TIME.
  float wedge = 6.28318 / max(floor(segments), 3.0);
  float rotated = angle + TIME * speed;
  float folded = abs(mod(rotated, wedge * 2.0) - wedge);

  vec2 newUv = vec2(cos(folded), sin(folded)) * radius + vec2(0.5);
  gl_FragColor = IMG_NORM_PIXEL(inputImage, newUv);
}
