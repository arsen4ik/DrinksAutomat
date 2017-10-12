(function () {
    var DrinksController = function ($scope, $http) {
        $scope.selectedItem = null;
        $scope.amountPaid = 0;
        $scope.selectItem = function (item) {
            $scope.selectedItem = item;
        };

        var getDrinks = function () {
            $http.get('/api/drinks')
                .then(function (response) {
                    $scope.Drinks = response.data;
                });

        };

        getNaminals = function () {
            $http.get('/api/naminals')
                .then(function (response) {
                    $scope.naminals = response.data;
                });

        };

        $scope.getAmountPaid = function () {
            $http.get('/api/amountpaid')
                .then(function (response) {
                    $scope.amountPaid = response.data;
                });

        };
        $scope.paid = function (naminal) {
            $http.post('/api/amountpaid', naminal)
                .then(function () {
                    $scope.getAmountPaid();
                    $scope.changeinfo = null;
                });

        };

        $scope.buy = function () {
            $http.put('/api/buy/'+ $scope.selectedItem.id, $scope.selectedItem)
                .then(function (response) {
                    $scope.getAmountPaid();
                    $scope.changeinfo = response.data;
                    getDrinks();
                    $scope.selectedItem = null;
                });

        };

        $scope.takechange = function () {
            $http.delete('/api/amountpaid')
                .then(function (response) {
                    $scope.getAmountPaid();
                    getDrinks();
                    $scope.changeinfo = response.data;
                    $scope.selectedItem = null;
                    $scope.amountPaid = 0;
                });

        };



        getNaminals();
        getDrinks();
        $scope.getAmountPaid();


    };
    
    drinksAutomatApp.controller('DrinksController', ['$scope', '$http', DrinksController]);

}());