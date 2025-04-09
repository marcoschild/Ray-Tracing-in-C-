# RayTracer in C

A minimal, fast ray tracing engine written in pure C — no external graphics libraries. This project demonstrates fundamental computer graphics techniques including:

- Vector math
- Ray-object intersection
- Diffuse shading
- Recursive reflections
- Simple PPM image output

---

## 🔧 Features

- Sphere rendering with Phong shading
- Camera ray casting
- Light source handling
- Multi-object scene support
- Outputs `.ppm` images viewable with any image viewer

---

## 📂 Project Structure

```
raytracer-c/
├── src/          # All source files
├── include/      # Header files
├── build/        # Compiled output
├── docs/         # Project documentation
├── Makefile
└── README.md
```

---

## ▶️ Build & Run

```bash
make
./build/raytracer
```

Image will be saved as `output.ppm`.

---

## 🛠 Tools Used

- Language: `C`
- Compiler: `gcc`
- Format: `PPM`

---

## 📜 License

MIT License
