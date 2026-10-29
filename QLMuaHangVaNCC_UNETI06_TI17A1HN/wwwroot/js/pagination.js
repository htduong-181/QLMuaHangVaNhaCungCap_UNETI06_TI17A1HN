
(function() {
    "use strict";

    window.initPagination = function(options = { }) {
        const table = document.querySelector(options.table);
        if (!table)
        {
            console.warn("Không tìm thấy bảng:", options.table);
            return;
        }

        const tbody = table.querySelector("tbody");
        if (!tbody) return;

        const rows = Array.from(tbody.querySelectorAll("tr"));
        const container = document.querySelector(options.pagination);

        if (!container) return;

        const pageSize = Math.max(1, Number(options.pageSize) || 5);
        const totalPages = Math.max(1, Math.ceil(rows.length / pageSize));
        let currentPage = 1;

        function render(page)
        {
            currentPage = Math.max(1, Math.min(page, totalPages));

            const start = (currentPage - 1) * pageSize;
            const end = start + pageSize;

            rows.forEach((row, index) => {
                row.hidden = index < start || index >= end;
                row.style.display =
                    index >= start && index < end ? "" : "none";
            });

            renderButtons();
        }

        function createButton(label, page, disabled, active = false)
        {
            const button = document.createElement("button");
            button.type = "button";
            button.textContent = label;
            button.className =
                "btn btn-sm " +
                (active ? "btn-primary" : "btn-outline-primary");

            button.disabled = disabled;
            button.addEventListener("click", () => render(page));

            return button;
        }

        function renderButtons()
        {
            container.innerHTML = "";
            container.classList.add(
                "d-flex", "gap-1", "justify-content-center",
                "align-items-center", "flex-wrap", "mt-3"
            );

            if (totalPages <= 1) return;

            container.appendChild(
                createButton("«", 1, currentPage === 1)
            );

            container.appendChild(
                createButton("‹", currentPage - 1, currentPage === 1)
            );

            let startPage = Math.max(1, currentPage - 2);
            let endPage = Math.min(totalPages, startPage + 4);
            startPage = Math.max(1, endPage - 4);

            for (let i = startPage; i <= endPage; i++)
            {
                container.appendChild(
                    createButton(i, i, false, i === currentPage)
                );
            }

            container.appendChild(
                createButton("›", currentPage + 1,
                    currentPage === totalPages)
            );

            container.appendChild(
                createButton("»", totalPages,
                    currentPage === totalPages)
            );
        }

        render(1);

        return {
        goToPage: render,
            refresh: () => render(1)
        }
        ;
    }
    ;
})();
