/**
 * INSPINIA - Responsive Admin Theme
 *
 * Functions (services)
 *  - httpService

 */

/**
 * httpService - services
 * Contains several global data used in different view
 *
 */


function httpService($http) {

    this.post = function (url, data) {
        var request = $http({
            method: "post",
            url: url,
            data: data
        });
        return request;
    }

    this.get = function (url, config) {
        return $http.get(url, config);
    }
}

function notificationService(toaster) {

    this.showmessage = function (title, message, type) {
        toaster.pop({
            type: type,
            title: title,
            body: message,
            showCloseButton: true,
            timeout: 5000,
        });
    }

}

angular
    .module('app')
    .service('httpService', httpService)
    .service('notificationService', notificationService);