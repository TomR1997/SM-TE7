//
//  MapLocationTest.swift
//  Grocerly.iOSTests
//
//  Created by issd on 19/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import XCTest
import MapKit
import Lottie
@testable import Grocerly_iOS

class MapLocationTest: XCTestCase {
    
    let expectedTitle: String = "Strijp TQ"
    let expectedSubtitle: String = "Strijp T"
    let expectedLocationName: String = "Leidl"
    let expectedDiscipline: String = "Derelict"
    let expectedCoordinate: CLLocationCoordinate2D = CLLocationCoordinate2D(latitude: 51.451299, longitude: 5.4535257)
    
    var mapLocation: MapLocation!

    override func setUp() {
        super.setUp()
        
        mapLocation = MapLocation(title: expectedTitle, locationName: expectedLocationName, discipline: expectedDiscipline, coordinate: expectedCoordinate, subtitle: expectedSubtitle)
    }

    override func tearDown() {
        super.tearDown()
    }

    func testTitle() {
        XCTAssertEqual(mapLocation.title, expectedTitle)
    }
    
    func testSubtitle() {
        XCTAssertEqual(mapLocation.subtitle, expectedSubtitle)
    }
    
    func testLocationName() {
        XCTAssertEqual(mapLocation.locationName, expectedLocationName)
    }
    
    func testDiscipline() {
        XCTAssertEqual(mapLocation.discipline, expectedDiscipline)
    }

    func testPerformanceExample() {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }

}
