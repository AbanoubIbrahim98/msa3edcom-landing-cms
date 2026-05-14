/* =========================================================
   msa3edcom — site.js
   Theme · navbar scroll · reveal · scroll spy · counters ·
   back-to-top · mobile nav close
   ========================================================= */

(function () {
    'use strict';

    const LS_THEME = 'msa3ed.theme';

    // ---------- Theme ----------
    const themeToggle = document.getElementById('themeToggle');

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        try { localStorage.setItem(LS_THEME, theme); } catch (e) { /* ignore */ }
    }

    function initTheme() {
        let saved = null;
        try { saved = localStorage.getItem(LS_THEME); } catch (e) { /* ignore */ }

        if (saved === 'light' || saved === 'dark') {
            applyTheme(saved);
        } else {
            const prefersDark = window.matchMedia &&
                window.matchMedia('(prefers-color-scheme: dark)').matches;
            applyTheme(prefersDark ? 'dark' : 'light');
        }
    }

    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            const current = document.documentElement.getAttribute('data-theme');
            applyTheme(current === 'dark' ? 'light' : 'dark');
        });
    }

    // ---------- Navbar scroll effect + back-to-top ----------
    const navbar = document.getElementById('siteNavbar');
    const backToTop = document.getElementById('backToTop');
    let ticking = false;

    function onScroll() {
        const y = window.scrollY;
        if (navbar) navbar.classList.toggle('scrolled', y > 20);
        if (backToTop) backToTop.classList.toggle('is-visible', y > 600);
        ticking = false;
    }

    window.addEventListener('scroll', function () {
        if (!ticking) {
            window.requestAnimationFrame(onScroll);
            ticking = true;
        }
    }, { passive: true });

    if (backToTop) {
        backToTop.addEventListener('click', () => {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }

    // ---------- Reveal on scroll ----------
    function initReveal() {
        const items = document.querySelectorAll('[data-reveal]');
        if (items.length === 0) return;

        if (!('IntersectionObserver' in window)) {
            items.forEach(el => el.classList.add('is-visible'));
            return;
        }

        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    const el = entry.target;
                    const delay = el.getAttribute('data-reveal-delay');
                    if (delay) el.style.setProperty('--reveal-delay', delay + 'ms');
                    el.classList.add('is-visible');
                    observer.unobserve(el);
                }
            });
        }, {
            threshold: 0.12,
            rootMargin: '0px 0px -40px 0px'
        });

        items.forEach(el => observer.observe(el));
    }

    // ---------- Counters ----------
    function initCounters() {
        const counters = document.querySelectorAll('[data-counter]');
        if (counters.length === 0) return;

        if (!('IntersectionObserver' in window)) {
            counters.forEach(el => el.textContent = el.getAttribute('data-target'));
            return;
        }

        const animate = (el) => {
            const targetRaw = el.getAttribute('data-target') || '0';
            const target = parseInt(targetRaw.replace(/[^0-9]/g, ''), 10);
            if (isNaN(target)) {
                el.textContent = targetRaw;
                return;
            }
            const duration = 1400;
            const start = performance.now();
            const step = (now) => {
                const t = Math.min(1, (now - start) / duration);
                const eased = 1 - Math.pow(1 - t, 3);
                el.textContent = Math.round(target * eased).toLocaleString();
                if (t < 1) requestAnimationFrame(step);
            };
            requestAnimationFrame(step);
        };

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    animate(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.4 });

        counters.forEach(el => observer.observe(el));
    }

    // ---------- Scroll spy ----------
    function initScrollSpy() {
        const navLinks = Array.from(document.querySelectorAll('[data-scrollspy] a.nav-link'));
        if (navLinks.length === 0) return;

        const sections = navLinks
            .map(a => document.querySelector(a.getAttribute('href')))
            .filter(Boolean);

        if (!('IntersectionObserver' in window) || sections.length === 0) return;

        const linkBySection = new Map();
        navLinks.forEach(a => {
            const target = document.querySelector(a.getAttribute('href'));
            if (target) linkBySection.set(target.id, a);
        });

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                const link = linkBySection.get(entry.target.id);
                if (!link) return;
                if (entry.isIntersecting && entry.intersectionRatio > 0.3) {
                    navLinks.forEach(l => l.classList.remove('is-active'));
                    link.classList.add('is-active');
                }
            });
        }, { threshold: [0.3, 0.6] });

        sections.forEach(section => observer.observe(section));
    }

    // ---------- Mobile nav auto-close ----------
    function initMobileNavClose() {
        const links = document.querySelectorAll('#mainNav .nav-link');
        const collapseEl = document.getElementById('mainNav');
        if (!collapseEl) return;

        links.forEach(function (link) {
            link.addEventListener('click', function () {
                if (collapseEl.classList.contains('show') &&
                    typeof bootstrap !== 'undefined') {
                    const bsCollapse = bootstrap.Collapse.getInstance(collapseEl);
                    if (bsCollapse) bsCollapse.hide();
                }
            });
        });
    }

    // ---------- FAQ exclusive open ----------
    function initFaq() {
        const items = document.querySelectorAll('.faq-list .faq-item');
        items.forEach(item => {
            item.addEventListener('toggle', () => {
                if (item.open) {
                    items.forEach(other => { if (other !== item) other.open = false; });
                }
            });
        });
    }

    // ---------- Lead form (AJAX submit) ----------
    function initLeadForm() {
        const form = document.getElementById('leadForm');
        if (!form) return;

        const alertBox = document.getElementById('leadFormAlert');
        const submitBtn = form.querySelector('.lead-submit');
        const fileInput = form.querySelector('input[type="file"][name="attachment"]');
        const fileLabel = form.querySelector('.lead-file-label');
        const fileLabelDefault = fileLabel ? fileLabel.textContent : '';

        if (fileInput && fileLabel) {
            fileInput.addEventListener('change', () => {
                const f = fileInput.files && fileInput.files[0];
                fileLabel.textContent = f ? f.name : fileLabelDefault;
            });
        }

        function showAlert(type, message) {
            if (!alertBox) return;
            alertBox.className = 'lead-form-alert is-' + type;
            alertBox.innerHTML = (type === 'success'
                ? '<i class="bi bi-check-circle-fill"></i>'
                : '<i class="bi bi-exclamation-triangle-fill"></i>') + ' <span>' + message + '</span>';
        }

        function clearAlert() {
            if (!alertBox) return;
            alertBox.className = 'lead-form-alert';
            alertBox.textContent = '';
        }

        function setLoading(loading) {
            if (!submitBtn) return;
            submitBtn.classList.toggle('is-loading', loading);
            submitBtn.disabled = loading;
        }

        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            clearAlert();

            const fullName = form.elements['FullName']?.value.trim();
            const email = form.elements['Email']?.value.trim();
            const message = form.elements['Message']?.value.trim();

            if (!fullName || !email || !message) {
                showAlert('error', form.dataset.errorRequired || 'Please fill in name, email, and message.');
                return;
            }
            const emailOk = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
            if (!emailOk) {
                showAlert('error', form.dataset.errorEmail || 'Email address is not valid.');
                return;
            }

            setLoading(true);
            try {
                const fd = new FormData(form);
                const res = await fetch(form.action, {
                    method: 'POST',
                    body: fd,
                    headers: { 'X-Requested-With': 'XMLHttpRequest', 'Accept': 'application/json' },
                    credentials: 'same-origin'
                });

                if (!res.ok) throw new Error('HTTP ' + res.status);
                const data = await res.json();

                if (data.success) {
                    form.reset();
                    if (fileLabel) fileLabel.textContent = fileLabelDefault;
                    showAlert('success', data.message);
                    // Subtle confirmation animation on submit button
                    submitBtn?.classList.add('is-success');
                    setTimeout(() => submitBtn?.classList.remove('is-success'), 1800);
                } else {
                    showAlert('error', data.message || 'Submission failed.');
                }
            } catch (err) {
                showAlert('error', 'Network error. Please try again.');
            } finally {
                setLoading(false);
            }
        });
    }

    // ---------- Init ----------
    document.addEventListener('DOMContentLoaded', function () {
        initTheme();
        initReveal();
        initCounters();
        initScrollSpy();
        initMobileNavClose();
        initFaq();
        initLeadForm();
        onScroll();
    });
})();
