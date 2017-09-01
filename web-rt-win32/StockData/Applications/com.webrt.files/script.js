

angular.module('FileManager', [])
    .factory('invokeVM', ['$q', function ($q) {
        return function (handler, action, data = null) {
            return $q(function (resolve, reject) {
                window.runtime.invoke(handler, action, data).then((result) => {
                    resolve(JSON.parse(result));
                }).catch((err) => reject(err));
            });
        };
    }])
    .factory('storage', ['invokeVM', function (invokeVM) {
        const CTRL = 'WebRT.Platform.Storage';
        const ACTION_GET_DRIVES = 'GetExternalDrives';
        const ACTION_GET_HOME = 'GetUserDirectory';
        const ACTION_GET_DIRS = 'GetDirectories';
        const ACTION_GET_FILES = 'GetFiles';

        return {
            getExternalDrives: () => invokeVM(CTRL, ACTION_GET_DRIVES),
            getHomeDirectory: () => invokeVM(CTRL, ACTION_GET_HOME),
            getFiles: (path) => invokeVM(CTRL, ACTION_GET_FILE, path),
            getDirectories: (path) => invokeVM(CTRL, ACTION_GET_DIRS, path)
        };
    }])
    .controller('FilesListController', ['storage', '$scope', function (storage, $scope) {
        this.folders = [];
        this.files = [];

        this.$onInit = () => {
            this.getRoot();
        };

        this.getRoot = async function () {
            this.folders = await storage.getExternalDrives();
            $scope.$apply();
        };

        this.navigate = async function (path) {
            this.folders = await storage.getDirectories(path);
            this.files = await storage.getFiles(path);
        };
    }]);