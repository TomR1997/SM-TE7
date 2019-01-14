//
//  MapLocation.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import MapKit

class MapLocation: NSObject, MKAnnotation {
    let title: String?
    let subtitle: String?
    let locationName: String
    let discipline: String
    let coordinate: CLLocationCoordinate2D
    
    init(
        title: String,
        locationName: String,
        discipline: String,
        coordinate: CLLocationCoordinate2D,
        subtitle: String
        ) {
        self.title = title
        self.locationName = locationName
        self.discipline = discipline
        self.coordinate = coordinate
        self.subtitle = subtitle
        
        super.init()
    }
}

