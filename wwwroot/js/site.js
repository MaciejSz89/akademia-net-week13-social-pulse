// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function validateImage(file, imageValidationElement) {

    const maxSize = 2 * 1024 * 1024;

    const allowedTypes = [
        "image/jpeg", "image/jpg", "image/png", "image/gif",
        "image/bmp", "image/webp", "image/tiff", "image/tif",
        "image/svg", "image/ico", "image/avif"
    ];

    if (!allowedTypes.includes(file.type)) {
        if (imageValidationElement) {
            imageValidationElement.text("Nieprawidłowy typ pliku. Dozwolone: JPEG, JPG, PNG, GIF, BMP, WEBP, TIFF, TIF, SVG, ICO, AVIF.").show();
        }
        return false; 
    }

    if (file.size > maxSize) {
        if (imageValidationElement) {
            const maxSizeMB = (maxSize / (1024 * 1024)).toFixed(1);
            imageValidationElement.text(`Rozmiar pliku nie może przekraczać ${maxSizeMB}MB.`).show();
        }
        return false; 
    }

    return true;
}
