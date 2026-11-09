document.addEventListener('DOMContentLoaded', function () {
  const toggle = document.getElementById('sidebarToggle');
  const backdrop = document.getElementById('sidebarBackdrop');
  const closeSidebar = () => { document.body.classList.remove('sidebar-open'); toggle?.setAttribute('aria-expanded', 'false'); };
  toggle?.addEventListener('click', () => { const isOpen = document.body.classList.toggle('sidebar-open'); toggle.setAttribute('aria-expanded', String(isOpen)); });
  backdrop?.addEventListener('click', closeSidebar);
  document.querySelectorAll('.admin-sidebar a').forEach(link => link.addEventListener('click', closeSidebar));
  window.addEventListener('keydown', e => { if (e.key === 'Escape') closeSidebar(); });
});
