//
//  ShoppingListTest.swift
//  Grocerly.iOSTests
//
//  Created by issd on 19/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import XCTest
@testable import Grocerly_iOS

class ShoppingListTest: XCTestCase {
    
    let expectedName: String = "MyShoppinglist"
    let expectedId: String = "1"
    let expectedImage: UIImage = UIImage(named: "tom")!
    let expectedDistance: Int = 5
    let expectedShoppingListItems: Int = 3
    
    var shoppinglist: ShoppingList!
    
    override func setUp() {
        super.setUp()
        
        shoppinglist = ShoppingList(name: expectedName, id: expectedId, image: expectedImage, distance: expectedDistance, shoppinglistItems: expectedShoppingListItems)
    }

    override func tearDown() {
        super.tearDown()
    }

    func testName() {
        XCTAssertEqual(shoppinglist.name, expectedName)
    }
    
    func testId(){
        XCTAssertEqual(shoppinglist.id, expectedId)
    }
    
    func testImage(){
        XCTAssertEqual(shoppinglist.image, expectedImage)
    }
    
    func testDistance(){
        XCTAssertEqual(shoppinglist.distance, expectedDistance)
    }
    
    func testShoppingListItems(){
        XCTAssertEqual(shoppinglist.shoppinglistItems, expectedShoppingListItems)
    }

    func testPerformanceExample() {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }

}
