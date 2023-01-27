angular.module('AvaTaxExtension')
    .factory('AvaTaxExtension.webApi', ['$resource', function ($resource) {
        return $resource('api/ava-tax-extension');
    }]);
