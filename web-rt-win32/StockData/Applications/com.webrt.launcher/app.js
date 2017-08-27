document.addEventListener('DOMContentLoaded', () => {
    const list = document.getElementById('appList');
    getPackages().then((packages) => {
        list.innerHTML += karkas.compile('appListItem', packages);

        document.querySelectorAll('[data-package-name]').forEach((i) => i.addEventListener('click', onItemClick));
    });

    document.body.addEventListener('click', (e) => {
        if (e.target.dataset.packageName !== undefined) {
            Launcher.startActivity(e.target.dataset.packageName);
        }
    });
});

async function getPackages() {
    const packages = await PackageManager.getPackages();
    return JSON.parse(packages);
}

function onItemClick() {
    Launcher.callActivity(this.dataset.packageName);
}
