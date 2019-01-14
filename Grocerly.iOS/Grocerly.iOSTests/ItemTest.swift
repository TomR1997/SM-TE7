//
//  ItemTest.swift
//  Grocerly.iOSTests
//
//  Created by issd on 19/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import XCTest
@testable import Grocerly_iOS

class ItemTest: XCTestCase {

    let expectedId: String = "Id"
    let expectedImageUrl: String = "imageUrl"
    let expectedName: String = "Kaas"
    let expectedPrice: Double = 5.0
    let expectedVolume: String = "500 gr"
    let expectedCreationDate: String = "12-12-2018"
    let expectedBarcode: Int = 31313
    
    var item: Item!
    
    override func setUp() {
        super.setUp()
        
        item = Item(id: expectedId, imageUrl: expectedImageUrl, name: expectedName, price: expectedPrice, volume: expectedVolume, creationDate: expectedCreationDate, barcode: expectedBarcode)
    }

    override func tearDown() {
        super.tearDown()
    }

    func testName() {
        XCTAssertEqual(item.name, expectedName)
    }
    
    func testImageUrl(){
        XCTAssertEqual(item.imageUrl, expectedImageUrl)
    }
    
    func testId() {
        XCTAssertEqual(item.id, expectedId)
    }
    
    func testPrice() {
        XCTAssertEqual(item.price, expectedPrice)
    }
    
    func testVolume(){
        XCTAssertEqual(item.volume, expectedVolume)
    }
    
    func testCreationDate() {
        XCTAssertEqual(item.creationDate, expectedCreationDate)
    }
    
    func testBarcode(){
        XCTAssertEqual(item.barcode, expectedBarcode)
    }

    func testPerformanceExample() {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }

}
