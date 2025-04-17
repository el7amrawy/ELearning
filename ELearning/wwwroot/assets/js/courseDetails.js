document.querySelectorAll('.border.rounded-lg .bg-gray-100').forEach(item => {
    item.addEventListener('click', () => {
        const content = item.nextElementSibling;
        const icon = item.querySelector('i');

        if (content.classList.contains('hidden')) {
            content.classList.remove('hidden');
            icon.classList.remove('fa-chevron-down');
            icon.classList.add('fa-chevron-up');
        } else {
            content.classList.add('hidden');
            icon.classList.remove('fa-chevron-up');
            icon.classList.add('fa-chevron-down');
        }
    });
});

document.querySelectorAll('.faq-accordion').forEach(accordion => {
    const button = accordion.querySelector('button');
    const content = accordion.querySelector('.faq-content');
    const icon = button.querySelector('i');

    button.addEventListener('click', (e) => {
        e.preventDefault(); // Prevent default button behavior

        // Close all other accordions
        document.querySelectorAll('.faq-accordion').forEach(otherAccordion => {
            if (otherAccordion !== accordion) {
                const otherContent = otherAccordion.querySelector('.faq-content');
                const otherIcon = otherAccordion.querySelector('i');
                otherContent.classList.add('hidden');
                otherIcon.classList.remove('rotate-180');
            }
        });

        // Toggle current accordion
        const isHidden = content.classList.contains('hidden');
        content.classList.toggle('hidden', !isHidden);
        icon.classList.toggle('rotate-180', isHidden);
    });
});