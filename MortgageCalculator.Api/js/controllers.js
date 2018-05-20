
function MainCtrl($http, $scope, authService, $location, $rootScope, $state, httpService) {

    
};




function indexCtrl($scope, DTOptionsBuilder, httpService) {

    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withDOM('<"html5buttons"B>Tfgitlp')
        .withButtons([
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },
            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]);

    $scope.getAllLoans = function () {

        var promiseGet = httpService.get(getAllLoansUrl, null);

        promiseGet.then(function (result) {
            debugger;
            $scope.loans = result.data;
        }, function (err) {
            Console.log(err);
        });
    }

    $scope.getAllLoans();
}

function calcCtrl($scope, httpService) {


}

angular
    .module('app')
    .controller('MainCtrl', MainCtrl)
    .controller('indexCtrl', indexCtrl)
    .controller('calcCtrl', calcCtrl);

