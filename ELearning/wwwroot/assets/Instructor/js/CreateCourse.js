// Image upload preview with fixed container
const fileUpload = document.getElementById('file-upload');
const imagePreview = document.getElementById('image-preview');
const uploadPlaceholder = document.getElementById('upload-placeholder');

fileUpload.addEventListener('change', function (e) {
    const file = e.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (event) {
            // Set the background image while maintaining container size
            imagePreview.style.backgroundImage = `url(${event.target.result})`;
            imagePreview.classList.remove('hidden');
            uploadPlaceholder.classList.add('hidden');

            // Ensure the preview maintains aspect ratio
            imagePreview.style.backgroundSize = 'cover';
            imagePreview.style.backgroundPosition = 'center';
            imagePreview.style.backgroundRepeat = 'no-repeat';
        }
        reader.readAsDataURL(file);
    } else {
        // If no file selected, revert to placeholder
        imagePreview.classList.add('hidden');
        uploadPlaceholder.classList.remove('hidden');
        imagePreview.style.backgroundImage = '';
    }
});