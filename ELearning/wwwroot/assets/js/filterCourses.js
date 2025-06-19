document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("search");
    const searchBtn = document.getElementById("searchBtn");
    const categorySelect = document.getElementById("category");
    const maxPriceInput = document.getElementById("maxPrice");
    const applyFiltersBtn = document.getElementById("applyFilters");
    const resultsContainer = document.getElementById("resultsContainer");
    const paginationContainer = document.getElementById("pagination");
    const prevPageBtn = document.getElementById("prevPage");
    const nextPageBtn = document.getElementById("nextPage");
    const pageNumbersContainer = document.getElementById("pageNumbers");

    let currentPage = 1;
    const pageSize = 10;
    let totalCourses = 0;

    fetchCourses();

    searchBtn.addEventListener("click", fetchCourses);
    applyFiltersBtn.addEventListener("click", fetchCourses);
    searchInput.addEventListener("keyup", function (e) {
        if (e.key === "Enter") fetchCourses();
    });
    prevPageBtn.addEventListener("click", goToPrevPage);
    nextPageBtn.addEventListener("click", goToNextPage);

    async function fetchCourses() {
        try {
            resultsContainer.innerHTML = `
                <div class="text-center py-12">
                    <i class="fas fa-spinner fa-spin text-4xl text-blue-500 mb-4"></i>
                    <p class="text-gray-600">Searching for courses...</p>
                </div>
            `;

            const apiUrl = buildApiUrl();

            const response = await fetch(apiUrl);
            const data = await response.json();

            displayCourses(data);
            totalCourses = data.length;
            updatePagination();
        } catch (error) {
            console.error("Error while fetching data:", error);
            resultsContainer.innerHTML = `
                <div class="text-center py-12 text-red-500">
                    <i class="fas fa-exclamation-circle text-4xl mb-4"></i>
                    <p class="text-lg">An error occurred while fetching data. Please try again.</p>
                </div>
            `;
        }
    }

    function buildApiUrl() {
        const searchTerm = searchInput.value.trim();
        const categoryId = categorySelect.value;
        const maxPrice = maxPriceInput.value;

        let apiUrl = `http://localhost:5120/courses/search?pageNumber=${currentPage}&pageSize=${pageSize}`;

        if (searchTerm) apiUrl += `&search=${encodeURIComponent(searchTerm)}`;
        if (categoryId !== "0") apiUrl += `&categoryId=${categoryId}`;
        if (maxPrice) apiUrl += `&maxPrice=${maxPrice}`;

        return apiUrl;
    }

    function displayCourses(courses) {
        if (!courses || courses.length === 0) {
            resultsContainer.innerHTML = `
                <div class="text-center py-12">
                    <i class="fas fa-book-open text-4xl text-gray-400 mb-4"></i>
                    <p class="text-gray-600 text-lg">No courses available matching your search.</p>
                    <p class="text-gray-500">Try changing your search terms or filters.</p>
                </div>
            `;
            return;
        }

        let html =
            '<div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">';

        courses.forEach((course) => {
            const priceDisplay =
                course.price === 0 ? "Free" : `$${course.price.toFixed(2)}`;
            const instructor = course.instructor || "N/A";
            const description =
                course.description || course.subTitle || "No description available";

            html += `
                <div class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-xl transition duration-300 min-h-[450px] flex flex-col">
                    <!-- Image container with aspect ratio 16:9 -->
                    <div class="relative w-full pt-[56.25%]">
                        <img src="${course.image.url}" 
                             alt="${course.title}" 
                             class="absolute top-0 left-0 w-full h-full object-cover bg-gray-100">
                    </div>
                    <div class="p-6 flex flex-col h-full">
                        <!-- Badge & Rating -->
                        <div class="flex items-center mb-2">
                            <span class="bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded">${course.levelName
                }</span>
                            <span class="ml-2 text-yellow-500">
                                <i class="fas fa-star"></i>
                                <i class="fas fa-star"></i>
                                <i class="fas fa-star"></i>
                                <i class="fas fa-star"></i>
                                <i class="far fa-star"></i>
                                <span class="text-gray-600 text-sm ml-1">(4.0)</span>
                            </span>
                        </div>
                        <!-- Course Title -->
                        <a href="/Courses/Details/${course.id || "unknown"}">
                            <h3 class="text-xl font-semibold mt-2 mb-1">${course.title
                }</h3>
                        </a>
                        <!-- Instructor -->
                        <div class="flex items-center text-sm text-gray-500 mb-3">${instructor}</div>
                        <!-- Description -->
                        <p class="text-gray-600 mb-4 flex-grow">${description}</p>
                        <!-- Price & Button -->
                        <div>
                            <div class="flex justify-between items-center mb-3">
                                <span class="font-bold text-blue-500">${priceDisplay}</span>
                                <span class="text-sm text-gray-500"><i class="fas fa-users mr-1"></i> ${course.enrollmentCount || "N/A"
                } enrolled</span>
                            </div>
                            <a class="w-full py-2 px-4 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition duration-300" href="/Cart/Add/${course.id || "unknown"
                }">
                                <i class="fas fa-shopping-cart mr-2"></i> Add to Cart
                            </a>
                        </div>
                    </div>
                </div>
            `;
        });

        html += "</div>";
        resultsContainer.innerHTML = html;
    }
    function updatePagination() {
        const totalPages = Math.ceil(totalCourses / pageSize);

        if (totalPages <= 1) {
            paginationContainer.classList.add("hidden");
            return;
        }

        paginationContainer.classList.remove("hidden");
        pageNumbersContainer.innerHTML = "";

        for (let i = 1; i <= totalPages; i++) {
            const pageBtn = document.createElement("button");
            pageBtn.className = `px-4 py-2 border-t border-b border-gray-300 ${i === currentPage
                    ? "bg-blue-50 text-blue-600 font-medium"
                    : "bg-white text-gray-600 hover:bg-gray-50"
                }`;
            pageBtn.textContent = i;
            pageBtn.addEventListener("click", () => {
                currentPage = i;
                fetchCourses();
            });
            pageNumbersContainer.appendChild(pageBtn);
        }
    }

    // Go to previous page
    function goToPrevPage() {
        if (currentPage > 1) {
            currentPage--;
            fetchCourses();
        }
    }

    // Go to next page
    function goToNextPage() {
        const totalPages = Math.ceil(totalCourses / pageSize);
        if (currentPage < totalPages) {
            currentPage++;
            fetchCourses();
        }
    }
});
