/* =========================================================
   msa3edcom — admin.js
   Sidebar · toasts · confirm modal · copy-URL · media picker
   ========================================================= */

(function () {
    'use strict';

    // ---------- Sidebar toggle on mobile ----------
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('adminSidebar');

    if (sidebarToggle && sidebar) {
        sidebarToggle.addEventListener('click', () => sidebar.classList.toggle('open'));

        document.querySelectorAll('.sidebar-link').forEach(link => {
            link.addEventListener('click', () => {
                if (window.innerWidth < 992) sidebar.classList.remove('open');
            });
        });
    }

    // ---------- Toasts ----------
    document.querySelectorAll('.toast-card .toast-close').forEach(btn => {
        btn.addEventListener('click', () => btn.closest('.toast-card')?.remove());
    });

    setTimeout(() => {
        document.querySelectorAll('.toast-success').forEach(t => {
            t.style.transition = 'opacity .4s, transform .4s';
            t.style.opacity = '0';
            t.style.transform = 'translateY(-8px)';
            setTimeout(() => t.remove(), 400);
        });
    }, 5000);

    // ---------- Confirm modal ----------
    function showConfirm(message) {
        return new Promise(resolve => {
            let backdrop = document.getElementById('adminConfirmBackdrop');
            if (!backdrop) {
                backdrop = document.createElement('div');
                backdrop.id = 'adminConfirmBackdrop';
                backdrop.className = 'admin-confirm-backdrop';
                backdrop.innerHTML = `
                    <div class="admin-confirm-modal">
                        <div class="admin-confirm-icon"><i class="bi bi-exclamation-triangle-fill"></i></div>
                        <h5 class="confirm-title mb-2">Are you sure?</h5>
                        <p class="text-muted small confirm-message mb-0"></p>
                        <div class="admin-confirm-actions">
                            <button type="button" class="btn btn-outline-secondary confirm-cancel">Cancel</button>
                            <button type="button" class="btn btn-danger confirm-ok">Delete</button>
                        </div>
                    </div>`;
                document.body.appendChild(backdrop);
            }
            backdrop.querySelector('.confirm-message').textContent = message || 'This action cannot be undone.';
            backdrop.classList.add('open');

            const close = (result) => {
                backdrop.classList.remove('open');
                resolve(result);
            };
            backdrop.querySelector('.confirm-ok').onclick = () => close(true);
            backdrop.querySelector('.confirm-cancel').onclick = () => close(false);
            backdrop.onclick = e => { if (e.target === backdrop) close(false); };
        });
    }

    document.body.addEventListener('submit', async e => {
        const form = e.target.closest('.confirm-delete');
        if (!form || form.dataset.confirmed === '1') return;
        e.preventDefault();
        const ok = await showConfirm(form.dataset.confirm || 'Delete this item?');
        if (ok) { form.dataset.confirmed = '1'; form.submit(); }
    }, true);

    // ---------- Copy URL ----------
    document.body.addEventListener('click', e => {
        const btn = e.target.closest('.copy-url');
        if (!btn) return;
        e.preventDefault();
        const url = btn.getAttribute('data-url');
        if (!url) return;
        const abs = url.startsWith('http') ? url : (window.location.origin + url);
        if (navigator.clipboard) {
            navigator.clipboard.writeText(abs).then(() => flashToast('URL copied'));
        }
    });

    function flashToast(message) {
        const stack = document.getElementById('toastStack');
        if (!stack) return;
        const t = document.createElement('div');
        t.className = 'toast-card toast-success';
        t.innerHTML = `<i class="bi bi-check-circle-fill"></i><span>${message}</span><button type="button" class="toast-close">&times;</button>`;
        t.querySelector('.toast-close').onclick = () => t.remove();
        stack.appendChild(t);
        setTimeout(() => { t.style.opacity = '0'; setTimeout(() => t.remove(), 400); }, 2500);
    }

    // ---------- Media picker ----------
    const modalEl = document.getElementById('mediaPickerModal');
    let mpModal = null;
    let mpTargetSelector = null;
    let mpDefaultCategory = '';
    let mpPage = 1;
    const mpGrid = document.getElementById('mpGrid');
    const mpEmpty = document.getElementById('mpEmpty');
    const mpPager = document.getElementById('mpPager');
    const mpSearch = document.getElementById('mpSearch');
    const mpCategory = document.getElementById('mpCategory');

    function ensureModal() {
        if (!mpModal && modalEl && typeof bootstrap !== 'undefined') {
            mpModal = new bootstrap.Modal(modalEl);
        }
        return mpModal;
    }

    async function loadMediaPicker(page = 1) {
        if (!mpGrid) return;
        mpPage = page;
        mpGrid.innerHTML = '<div class="text-center text-muted py-5"><i class="bi bi-arrow-clockwise"></i> Loading…</div>';

        const params = new URLSearchParams({
            page: page,
            category: mpCategory.value || '',
            q: mpSearch.value || ''
        });

        try {
            const res = await fetch('/Media/List?' + params.toString(), { headers: { 'Accept': 'application/json' } });
            const data = await res.json();
            renderMediaTiles(data);
        } catch (e) {
            mpGrid.innerHTML = '<div class="text-center text-danger py-5">Failed to load media.</div>';
        }
    }

    function renderMediaTiles(data) {
        const items = data.items || [];
        if (items.length === 0) {
            mpGrid.innerHTML = '';
            mpEmpty.classList.remove('d-none');
            mpPager.innerHTML = '';
            return;
        }
        mpEmpty.classList.add('d-none');
        mpGrid.innerHTML = items.map(m => `
            <div class="mp-tile" data-url="${m.url}" data-alt="${m.alt || ''}" title="${m.name}">
                <img src="${m.url}" alt="${m.alt || ''}" loading="lazy" />
            </div>`).join('');

        mpGrid.querySelectorAll('.mp-tile').forEach(tile => {
            tile.addEventListener('click', () => {
                const url = tile.getAttribute('data-url');
                const target = mpTargetSelector ? document.querySelector(mpTargetSelector) : null;
                if (target) {
                    target.value = url;
                    target.dispatchEvent(new Event('change', { bubbles: true }));

                    // If there's an image-preview sibling, update it
                    const preview = target.closest('.col-md-4, .col-md-6, .col-md-3, .col-12, .input-group')?.parentElement?.querySelector('.image-preview, .image-preview-lg, .image-preview-live');
                    if (preview) preview.querySelector('img').src = url;
                }
                ensureModal()?.hide();
            });
        });

        if (data.totalPages > 1) {
            const buttons = [];
            for (let i = 1; i <= data.totalPages; i++) {
                buttons.push(`<button data-page="${i}" class="${i === data.page ? 'is-active' : ''}">${i}</button>`);
            }
            mpPager.innerHTML = buttons.join('');
            mpPager.querySelectorAll('button').forEach(b => {
                b.addEventListener('click', () => loadMediaPicker(parseInt(b.dataset.page, 10)));
            });
        } else {
            mpPager.innerHTML = '';
        }
    }

    let mpSearchTimer = null;
    if (mpSearch) {
        mpSearch.addEventListener('input', () => {
            clearTimeout(mpSearchTimer);
            mpSearchTimer = setTimeout(() => loadMediaPicker(1), 280);
        });
    }
    if (mpCategory) {
        mpCategory.addEventListener('change', () => loadMediaPicker(1));
    }

    document.querySelectorAll('[data-media-picker]').forEach(btn => {
        btn.addEventListener('click', () => {
            mpTargetSelector = btn.getAttribute('data-media-target');
            mpDefaultCategory = btn.getAttribute('data-media-category') || '';
            if (mpCategory) mpCategory.value = mpDefaultCategory;
            if (mpSearch) mpSearch.value = '';
            ensureModal()?.show();
            loadMediaPicker(1);
        });
    });

    // ---------- Live preview for file inputs ----------
    document.querySelectorAll('input[type="file"][accept*="image"]').forEach(input => {
        if (input.dataset.previewBound === '1') return;
        input.dataset.previewBound = '1';
        input.addEventListener('change', e => {
            const file = e.target.files[0];
            if (!file) return;
            const reader = new FileReader();
            reader.onload = ev => {
                let host = input.parentElement;
                let preview = host.querySelector('.image-preview-live img');
                if (!preview) {
                    const wrap = document.createElement('div');
                    wrap.className = 'image-preview-live mt-2';
                    wrap.innerHTML = '<img style="max-height:120px; border-radius:8px;" />';
                    host.appendChild(wrap);
                    preview = wrap.querySelector('img');
                }
                preview.src = ev.target.result;
            };
            reader.readAsDataURL(file);
        });
    });
})();
