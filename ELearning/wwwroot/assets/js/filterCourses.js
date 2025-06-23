document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("search");
    const searchBtn = document.getElementById("searchBtn");
    const categorySelect = document.getElementById("category");
    const minPriceInput = document.getElementById("minPrice");
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
    let categories = [];

    const baseURL = "https://embedskill.com";

    // Initialize the page
    initPage();

    async function initPage() {
        try {
            await fetchCategories();
            fetchCourses();
        } catch (error) {
            console.error("Initialization error:", error);
            showErrorState("Failed to initialize page. Please refresh.");
        }
    }

    // Fetch all categories from the API
    async function fetchCategories() {
        try {
            showLoadingState("Loading categories...");
            const response = await fetch(
                baseURL + "/dashboard/categories/getall"
            );

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            categories = await response.json();

            if (!Array.isArray(categories) || categories.length === 0) {
                throw new Error("No categories returned from API");
            }

            populateCategoryDropdown();
        } catch (error) {
            console.error("Error fetching categories:", error);
            categorySelect.innerHTML =
                '<option value="0">Failed to load categories</option>';
            throw error;
        }
    }

    function populateCategoryDropdown() {
        categorySelect.innerHTML = "";

        // Add default "All Categories" option
        const defaultOption = document.createElement("option");
        defaultOption.value = "0";
        defaultOption.textContent = "All Categories";
        categorySelect.appendChild(defaultOption);

        categories.forEach((category) => {
            if (!category.id || !category.name) return;

            const option = document.createElement("option");
            option.value = category.id;
            option.textContent = category.name;
            categorySelect.appendChild(option);
        });
    }

    searchBtn.addEventListener("click", () => {
        currentPage = 1;
        fetchCourses();
    });

    applyFiltersBtn.addEventListener("click", () => {
        currentPage = 1;
        fetchCourses();
    });

    searchInput.addEventListener("keyup", function (e) {
        if (e.key === "Enter") {
            currentPage = 1;
            fetchCourses();
        }
    });

    prevPageBtn.addEventListener("click", goToPrevPage);
    nextPageBtn.addEventListener("click", goToNextPage);

    async function fetchCourses() {
        try {
            showLoadingState();

            const apiUrl = buildApiUrl();
            const response = await fetch(apiUrl);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            displayCourses(data);
            totalCourses = data.length;
            updatePagination();
        } catch (error) {
            console.error("Error while fetching data:", error);
            showErrorState(
                "An error occurred while fetching courses. Please try again."
            );
        }
    }

    function buildApiUrl() {
        const queryString = GetQueryString("search");
        let searchTerm = searchInput.value.trim();
        if (queryString.length > 0) {
            searchTerm = queryString;
            searchInput.value = queryString;
            RemoveQueryString();
        }

        const categoryId = categorySelect.value;
        const minPrice = minPriceInput.value;
        const maxPrice = maxPriceInput.value;

        let apiUrl = `${baseURL}/courses/search?pageNumber=${currentPage}&pageSize=${pageSize}`;

        if (searchTerm) {
            apiUrl += `&search=${encodeURIComponent(searchTerm)}`;
        }

        if (categoryId && categoryId !== "0") {
            apiUrl += `&categoryId=${categoryId}`;
        }

        if (minPrice) {
            apiUrl += `&minPrice=${minPrice}`;
        }
        if (maxPrice) {
            apiUrl += `&maxPrice=${maxPrice}`;
        }

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
            const instructor = course.instructor
                ? `${course.instructor.firstName} ${course.instructor.lastName}`
                : "Unknown Instructor";
            const description =
                course.description || course.subTitle || "No description available";
            const imageUrl =
                course.image?.url ||
                "https://via.placeholder.com/300x169?text=Course+Image";
            const courseId = course.id || "unknown";
            const levelName = course.levelName || "All Levels";

            html += `
                        <div class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-xl transition duration-300 min-h-[450px] flex flex-col">
                            <div class="relative w-full pt-[56.25%]">
                                <img src="${imageUrl}" 
                                     alt="${course.title}" 
                                     class="absolute top-0 left-0 w-full h-full object-cover bg-gray-100">
                            </div>
                            <div class="p-6 flex flex-col h-full">
                                <div class="flex items-center mb-2">
                                    <span class="bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded">${levelName}</span>
                                    <span class="ml-2 text-yellow-500">
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="fas fa-star"></i>
                                        <i class="far fa-star"></i>
                                        <span class="text-gray-600 text-sm ml-1">(4.0)</span>
                                    </span>
                                </div>
                                <a href="/Courses/Details/${courseId}">
                                    <h3 class="text-xl font-semibold mt-2 mb-1">${course.title || "Untitled Course"
                }</h3>
                                </a>
                                <div class="flex items-center text-sm text-gray-500 mb-3">${instructor}</div>
                                <p class="text-gray-600 mb-4 flex-grow">${description}</p>
                                <div>
                                    <div class="flex justify-between items-center mb-3">
                                        <span class="font-bold text-blue-500">${priceDisplay}</span>
                                    </div>
                                    <a class="w-full py-2 px-4 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition duration-300" href="/Cart/Add/${courseId}">
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

        prevPageBtn.disabled = currentPage === 1;

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

        nextPageBtn.disabled = currentPage === totalPages;
    }

    function goToPrevPage() {
        if (currentPage > 1) {
            currentPage--;
            fetchCourses();
        }
    }

    function goToNextPage() {
        const totalPages = Math.ceil(totalCourses / pageSize);
        if (currentPage < totalPages) {
            currentPage++;
            fetchCourses();
        }
    }

    function showLoadingState(message = "Loading...") {
        resultsContainer.innerHTML = `
                    <div class="text-center py-12">
                        <i class="fas fa-spinner fa-spin text-4xl text-blue-500 mb-4"></i>
                        <p class="text-gray-600">${message}</p>
                    </div>
                `;
    }

    function showErrorState(message) {
        resultsContainer.innerHTML = `
                    <div class="text-center py-12 text-red-500">
                        <i class="fas fa-exclamation-circle text-4xl mb-4"></i>
                        <p class="text-lg">${message}</p>
                    </div>
                `;
    }
});

function GetQueryString(query) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(query) ?? "";
}

function RemoveQueryString() {
    window.history.replaceState({}, document.title, window.location.pathname);
}
