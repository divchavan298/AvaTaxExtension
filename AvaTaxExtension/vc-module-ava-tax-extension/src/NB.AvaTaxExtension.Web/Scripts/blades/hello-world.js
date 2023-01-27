angular.module('AvaTaxExtension')
    .controller('AvaTaxExtension.helloWorldController', ['$scope', 'AvaTaxExtension.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'AvaTaxExtension';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'AvaTaxExtension.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
