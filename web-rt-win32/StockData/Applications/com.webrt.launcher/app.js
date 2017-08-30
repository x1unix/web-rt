const PM_HANDLER = "WebRT.Platform.PackageManager";
const PM_ACTION = "GetPackagesList";

document.addEventListener('DOMContentLoaded', () => {
    const list = document.getElementById('appList');
    getPackages().then((data) => {
        console.log(data);

        const packages = JSON.parse(data);
        list.innerHTML += karkas.compile('appListItem', packages);

        document.querySelectorAll('[data-package-name]').forEach((i) => i.addEventListener('click', onItemClick));
    });
   

    document.body.addEventListener('click', (e) => {
        if (e.target.dataset.packageName !== undefined) {
            Launcher.startActivity(e.target.dataset.packageName);
        }
    });
});

async function execSysCall(handler, action, data) {
    return await runtimeBridge.invoke(handler, action, data);
}

async function getPackages() {
    return await execSysCall("WebRT.Platform.PackageManager", "GetPackagesList", null);
}

function onItemClick() {
    // Launcher.callActivity(this.dataset.packageName);
    execSysCall("WebRT.Platform.Launcher", "StartApplication", this.dataset.packageName);
}
