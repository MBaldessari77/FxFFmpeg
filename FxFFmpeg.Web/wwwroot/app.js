(function (window, angular) {
  'use strict';
  angular.module('RxFFmpeg', ['ngMaterial', 'ngMessages', 'ngRoute'])
    .config(function ($mdThemingProvider, $routeProvider) {
      $mdThemingProvider.theme('dark-blue').backgroundPalette('blue').dark();

      $routeProvider
        .when('/tab/:tabId', {
          templateUrl: 'tab.html',
          controller: 'tabController'
        });

    })
    .controller('RxFFmpegCtrl',
    function ($scope, $http, $mdDialog, $mdMedia, $location) {
      $scope.states = {
        versions: null,
        tab: {}
      };

      $scope.$location = $location;

      function getFolderContent(path, includeSubFolders) {
        return $http.get('api/MediaFiles/GetFiles?path=' + path + '&includeSubFolders=' + includeSubFolders)
          .then(function (result) {
            return result.data;
          });
      }

      function getMediaInfo(path) {
        return $http.get('api/FFmpeg/GetMediaInfo?path=' + path)
          .then(function (result) {
            return result.data;
          });
      }

      function loadMediaInfo(files, i) {
        return getMediaInfo(files[i].path)
          .then(function (info) {
            files[i].mediaInfo = info;
          });
      }

      $scope.actions = {
        openMenu: function ($mdMenu, ev) {
          $mdMenu.open(ev);
        },
        loadFolder: function (tab) {
          tab.waitingServer = true;
          tab.loadComplete = -1;
          tab.files = [];
          getFolderContent(tab.path, tab.includeSubFolders).then(function (files) {
            tab.loadComplete = files.length > 0 ? 0 : -1;
            var current = 0;
            for (var j = 0; j < files.length; j++) {
              if (files[j].name.length > 30) {
                files[j].tooltip = files[j].name;
                files[j].name = files[j].name.substr(0, 30) + '...';
              }
              loadMediaInfo(files, j).then(function () {
                tab.loadComplete = ++current * 100 / files.length;
              });
            }
            tab.files = files;
            tab.waitingServer = false;
          });
        },
        openFolder: function (ev) {
          var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
          $mdDialog.show({
            templateUrl: 'openFolder.tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
          })
            .then(function (path) {
              if ($scope.states.tab.path && $scope.states.tab.path === path)
                return;
              var encodedPath = encodeURIComponent(encodeURIComponent(path));
              $location.path('/tab/' + encodedPath);
            })
            .catch(function () {
            });
          $scope.$watch(function () {
            return $mdMedia('xs') || $mdMedia('sm');
          },
            function (wantsFullScreen) {
              $scope.customFullscreen = wantsFullScreen === true;
            });
        }
      };

      $http.get('api/info/version')
        .then(function (result) {
          $scope.states.version = result.data;
        });

      $http.get('api/info/versions')
        .then(function (result) {
          $scope.states.versions = result.data;
        });

    })
    .controller("tabController", function ($scope, $routeParams) {
      var path = decodeURIComponent(decodeURIComponent($routeParams.tabId));
      var tab = { type: 'folder', path: path, includeSubFolders: false, waitingServer: false, loadComplete: -1 };
      $scope.states.tab = tab;
      $scope.actions.loadFolder(tab);
    })
    .controller("openFolderDialogController",
    function ($scope, $http, $mdDialog) {

      $scope.states = {
        waitingServer: false
      };

      $scope.model = {
        path: ''
      };

      $scope.actions = {
        suggestPath: function (path) {
          $http.get('api/path/SuggestPath?path=' + path)
            .then(function (result) {
              $scope.model.path = result.data;
            });
        },
        cancel: function () {
          $mdDialog.cancel();
        },
        ok: function (form) {
          $scope.states.waitingServer = true;
          $http.get('api/path/DirectoryExists?path=' + $scope.model.path)
            .then(function (result) {
              if (result.data)
                $mdDialog.hide($scope.model.path);
              else
                form.path.$error.invalidDirectory = true;
            })
            .finally(function () {
              $scope.states.waitingServer = false;
            });
        }
      };

    })
    .directive('captureTab',
    function () {
      return {
        scope: {
          onTab: '&'
        },
        restrict: 'A',
        link: function ($scope, element) {
          element.bind('keydown keypress',
            function (event) {
              if (event.which === 9) { // Tab
                event.preventDefault();
                $scope.onTab({});
              }
            });
        }
      };
    });
})(window, window.angular);