#include <stdio.h>
#include "ray.h"
#include "vec3.h"

int main() {
    printf("Ray Tracer Initialized\n");

    Vec3 origin = {0, 0, 0};
    Vec3 direction = {0, 0, -1};
    Ray ray = {origin, direction};

    printf("Ray origin: (%.2f, %.2f, %.2f)\n", ray.origin.x, ray.origin.y, ray.origin.z);
    printf("Ray direction: (%.2f, %.2f, %.2f)\n", ray.direction.x, ray.direction.y, ray.direction.z);

    FILE *f = fopen("output.ppm", "w");
    fprintf(f, "P3\n3 2\n255\n255 0 0\n0 255 0\n0 0 255\n255 255 0\n255 255 255\n0 0 0\n");
    fclose(f);

    printf("Rendered image written to output.ppm\n");

    return 0;
}
