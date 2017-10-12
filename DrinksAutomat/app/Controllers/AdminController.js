(function () {
    var AdminController = function ($scope, $http) {
        $scope.isChanged = false;
        $scope.saving = false;
        $scope.change = function () {
            $scope.isChanged = true;
        };
        $scope.cancel = function () {
            getAmount();
            getNaminals();
            getDrinks();
            $scope.isChanged = false;
        };
        $scope.save = function () {
            $scope.saving = true;
            $scope.isChanged = false;
            angular.forEach($scope.Drinks, function (value, index) {
                $http.put('/api/drinks/' + value.id, value);
            });
            angular.forEach($scope.naminals, function (value, index) {
                $http.put('/api/naminals/' + value.id, value);
            });

            $http.post('/api/amount', $scope.amount)
                .then(function (response) { $scope.saving = false; });

           
            
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
        getAmount = function () {
            $http.get('/api/amount')
                .then(function (response) {
                    $scope.amount = response.data;
                });

        };

        getAmount();
        getNaminals();
        getDrinks();
    };
    


    drinksAutomatApp.controller('AdminController', ['$scope', '$http', AdminController]);

}());