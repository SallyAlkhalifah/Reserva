window.reservaAnimations = {

    init: function () {

        /* =========================================
           HEADER SCROLL
        ========================================= */

        const header = document.querySelector(".main-header");

        if (header) {

            const handleHeaderScroll = () => {

                if (window.scrollY > 40) {
                    header.classList.add("header-scrolled");
                } else {
                    header.classList.remove("header-scrolled");
                }

            };

            window.addEventListener("scroll", handleHeaderScroll, {
                passive: true
            });

            handleHeaderScroll();
        }


        /* =========================================
           SCROLL REVEAL
        ========================================= */

        const revealElements =
            document.querySelectorAll(".scroll-reveal");

        if (revealElements.length > 0) {

            const observer = new IntersectionObserver(
                (entries) => {

                    entries.forEach((entry) => {

                        if (entry.isIntersecting) {

                            entry.target.classList.add("is-visible");

                            observer.unobserve(entry.target);
                        }

                    });

                },
                {
                    threshold: 0.12
                }
            );

            revealElements.forEach((element) => {
                observer.observe(element);
            });
        }
    }
};