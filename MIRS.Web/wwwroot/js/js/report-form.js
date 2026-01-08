// Handle image upload and preview
function handleImageUpload(event) {
    const files = event.target.files;
    const preview = document.getElementById('imagePreview');

    // Clear existing preview if needed
    preview.innerHTML = '';

    // Limit to 5 images
    const maxImages = 5;
    const imagesToShow = Math.min(files.length, maxImages);

    for (let i = 0; i < imagesToShow; i++) {
        const file = files[i];

        // Check file size (max 5MB)
        if (file.size > 5 * 1024 * 1024) {
            alert('Image ' + file.name + ' is too large. Maximum size is 5MB');
            continue;
        }

        const reader = new FileReader();

        reader.onload = function(e) {
            const previewItem = document.createElement('div');
            previewItem.className = 'preview-item';

            const img = document.createElement('img');
            img.src = e.target.result;
            img.alt = 'Preview';

            const removeBtn = document.createElement('div');
            removeBtn.className = 'remove-image';
            removeBtn.innerHTML = '×';
            removeBtn.onclick = function() {
                previewItem.remove();
            };

            previewItem.appendChild(img);
            previewItem.appendChild(removeBtn);
            preview.appendChild(previewItem);
        };

        reader.readAsDataURL(file);
    }

    if (files.length > maxImages) {
        alert('Only first ' + maxImages + ' images uploaded. Maximum attachments is ' + maxImages + ' images');
    }
}

// Form submission
document.getElementById('reportForm').addEventListener('submit', function(e) {
    e.preventDefault();

    // Basic validation
    const title = document.getElementById('problemTitle').value;
    const description = document.getElementById('problemDescription').value;
    const municipality = document.getElementById('municipality').value;
    const location = document.getElementById('locationDescription').value;

    if (!title || !description || !municipality || !location) {
        alert('Please fill all required fields');
        return;
    }

    // Simulate form submission
    const submitBtn = this.querySelector('button[type="submit"]');
    const originalText = submitBtn.innerHTML;

    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i> Submitting...';
    submitBtn.disabled = true;

    // Simulate API call
    setTimeout(function() {
        alert('Report submitted successfully! Report Number: #' + Math.floor(Math.random() * 10000) + '\nYour report will be processed as soon as possible.');
        submitBtn.innerHTML = originalText;
        submitBtn.disabled = false;

        // Reset form
        document.getElementById('reportForm').reset();
        document.getElementById('imagePreview').innerHTML = '';
    }, 2000);
});

// Initialize WOW.js for animations
new WOW().init();