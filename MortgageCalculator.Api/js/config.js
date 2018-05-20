
function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, IdleProvider, KeepaliveProvider) {

    // Configure Idle settings
    IdleProvider.idle(5); // in seconds
    IdleProvider.timeout(120); // in seconds

    $urlRouterProvider.otherwise("/mortgage/index");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: false
    });

    $stateProvider
         .state('mortgage', {
             abstract: true,
             url: "/mortgage",
             templateUrl: "pages/common/content.html",
         })

        .state('mortgage.index', {
            url: "/index",
            templateUrl: "pages/mortgage/index.html",
            data: { pageTitle: 'index' },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            serie: true,
                            files: ['js/plugins/dataTables/datatables.min.js', 'css/plugins/dataTables/datatables.min.css']
                        },
                        {
                            serie: true,
                            name: 'datatables',
                            files: ['js/plugins/dataTables/angular-datatables.min.js']
                        },
                        {
                            serie: true,
                            name: 'datatables.buttons',
                            files: ['js/plugins/dataTables/angular-datatables.buttons.min.js']
                        }
                    ]);
                }
            }
        })

     .state('mortgage.calc', {
         url: "/calculation",
         templateUrl: "pages/mortgage/calculation.html",
         data: { pageTitle: 'Static table' },
         resolve: {
             loadPlugin: function ($ocLazyLoad) {
                 return $ocLazyLoad.load([
                     {
                         name: 'angular-peity',
                         files: ['js/plugins/peity/jquery.peity.min.js', 'js/plugins/peity/angular-peity.js']
                     },
                     {
                         files: ['css/plugins/iCheck/custom.css', 'js/plugins/iCheck/icheck.min.js']
                     }
                 ]);
             }
         }
     })

    ;

     

}
angular
    .module('app')
    .config(config)
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    })
    .run(function ($rootScope, $state, $location) {
        $rootScope.$state = $state;

        var username = null;
        var companyid = null;

        $rootScope.getusername = function () {
            if (!username) username = sessionStorage.getItem('userName');
            return username;
        };

        $rootScope.getcompany = function () {
            if (!companyid) companyid = sessionStorage.getItem('companyid');
            return companyid;
        };
      
    })
    .run(['authService', function (authService) {
        authService.fillAuthData();
    }]);
