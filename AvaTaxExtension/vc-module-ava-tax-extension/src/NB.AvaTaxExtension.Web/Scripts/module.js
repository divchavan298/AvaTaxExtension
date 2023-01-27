// Call this to register your module to main application
var moduleName = 'AvaTaxExtension';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.AvaTaxExtensionState', {
                    url: '/AvaTaxExtension',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'AvaTaxExtension.helloWorldController',
                                template: 'Modules/$(NB.AvaTaxExtension)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/AvaTaxExtension',
                icon: 'fa fa-cube',
                title: 'AvaTaxExtension',
                priority: 100,
                action: function () { $state.go('workspace.AvaTaxExtensionState'); },
                permission: 'AvaTaxExtension:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
