# Instrucciones para el Logo del INEEL

## Archivos de imagen necesarios

Para completar la personalización visual, necesitas agregar los siguientes archivos de imagen en la carpeta:

?? `SistemaINEEL/wwwroot/images/`

### Archivos requeridos:

1. **logo-ineel.png** - Logo principal del INEEL (con fondo transparente)
   - Usado en: Navbar y Footer
   - Tamaño recomendado: 200x80 píxeles aprox.

2. **logo-ineel-white.png** - Logo del INEEL en versión blanca (para fondos oscuros)
   - Usado en: Página de Login
   - Tamaño recomendado: 200x80 píxeles aprox.
   - Si no tienes versión blanca, el sistema aplicará un filtro CSS automático

## Colores corporativos aplicados

Se han utilizado los siguientes colores basados en el logo del INEEL:

| Color | Código HEX | Uso |
|-------|------------|-----|
| Verde INEEL | `#4A7C59` | Color principal, acentos, bordes |
| Azul INEEL | `#1E5AA8` | Color secundario, encabezados |
| Verde oscuro | `#3A6347` | Estados hover |
| Azul oscuro | `#164785` | Estados hover |

## Gradiente corporativo

```css
--gradient-ineel: linear-gradient(135deg, #4A7C59 0%, #1E5AA8 100%);
```

## Cambios realizados

### Archivos CSS actualizados:
- `wwwroot/css/layout.css` - Navbar, Footer y elementos generales
- `wwwroot/css/login.css` - Página de inicio de sesión
- `wwwroot/css/home.css` - Dashboard principal
- `wwwroot/css/reportes.css` - Vista de reportes
- `wwwroot/css/crear.css` - Vista de crear consecutivo (nuevo)

### Vistas actualizadas:
- `Views/Shared/_Layout.cshtml` - Layout principal con logo
- `Views/Login/Index.cshtml` - Página de login con branding INEEL
- `Views/Acciones/Crear.cshtml` - Formulario rediseñado

### JavaScript actualizado:
- `wwwroot/js/reportes.js` - Usando SweetAlert2
- `wwwroot/js/acciones-crear.js` - Usando SweetAlert2

### Librerías agregadas:
- Font Awesome 6.5.1 (CDN)
- SweetAlert2 (CDN)

## Cómo agregar el logo

1. Descarga o exporta el logo del INEEL en formato PNG con fondo transparente
2. Renómbralo a `logo-ineel.png`
3. Cópialo a la carpeta `SistemaINEEL/wwwroot/images/`
4. Opcionalmente, crea una versión blanca llamada `logo-ineel-white.png`

## Notas

- Si el logo no se encuentra, el sistema mostrará un ícono de respaldo
- Los colores se pueden ajustar modificando las variables CSS en `:root`
