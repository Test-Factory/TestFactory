function CategoryModel(item) { 
    var self = this;

   // var markProvider = new MarkProvider();

    self.id = ko.observable(item ? item.Id : "");
    self.name = ko.observable(item ? item.Name : "");
    self.code = ko.observable(item ? item.Code : "");
    self.averageMark = ko.observable("");
    //if (item) {
    //    markProvider.get(self.id(), function(data) {
    //            self.averageMark(data);
    //        });
    //}
}