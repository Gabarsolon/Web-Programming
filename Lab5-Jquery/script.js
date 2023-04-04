function test() {
    console.log('function test()');
    }
    
    window.setTimeout(test, 0);
    // Code in normal notation:
    //var promise = new Promise(function(resolve, reject) {
    //	var i = 0;
    //	resolve('promised resolved.');
    //});
    //promise.then(/*resolve func*/ function(param) {console.log(param)},
    //			 /*reject func*/ function(param) {console.log(param)});
    // Same code as above, but in fat arrow notation:
    var promise = new Promise((resolve, reject) => {
        var i = 0;
        resolve('promised resolved.');
    });
    promise.then(/*resolve func*/ param => console.log(param),
         /*reject func*/ param => console.log(param));
    // we could have skipped the reject function argument from above as it is not used.
    
    console.log('end example.');
    // Output:
    //		end example.
    //		promised resolved.
    //		function test()