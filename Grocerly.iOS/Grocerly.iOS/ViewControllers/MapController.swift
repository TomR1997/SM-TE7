//
//  MapController.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit
import MapKit

class MapController : UIViewController, MKMapViewDelegate {
    
    @IBOutlet weak var directionsMap: MKMapView!
    
    let initialLocation = CLLocation(latitude: 51.451299, longitude: 5.4535257)
    let regionRadius: CLLocationDistance = 500
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        directionsMap.delegate = self
        
        directionsMap.showsUserLocation = true
        
        let tq = MapLocation(
            title: "Strijp TQ",
            locationName: "Strijp T",
            discipline: "Derelict",
            coordinate: CLLocationCoordinate2D(latitude: 51.451299, longitude: 5.4535257),
            subtitle: "Strijp T")
        
        directionsMap.addAnnotation(tq)
        centerMapOnLocation(location: initialLocation)
        
        let request = MKDirections.Request()
        request.source = MKMapItem(placemark: MKPlacemark(coordinate: CLLocationCoordinate2D(latitude: 51.451299, longitude: 5.4535257), addressDictionary: nil))
        request.destination = MKMapItem(placemark: MKPlacemark(coordinate: CLLocationCoordinate2D(latitude: 51.44072329999999, longitude: 5.453145599999971), addressDictionary: nil))
        request.requestsAlternateRoutes = true
        request.transportType = .automobile
        
        let location = request.destination
        location?.name = "Albert Hijen"
        let launchOptions = [MKLaunchOptionsDirectionsModeKey: MKLaunchOptionsDirectionsModeDriving]
        location!.openInMaps(launchOptions: launchOptions)
        
        let directions = MKDirections(request: request)
        
        directions.calculate(completionHandler: {(response, error) in
            
            if error != nil {
                print("Error getting directions")
            } else {
                self.showRoute(response!)
            }
        })
    }
    
    func showRoute(_ response: MKDirections.Response) {
        
        for route in response.routes {
            
            directionsMap.addOverlay(route.polyline,
                         level: MKOverlayLevel.aboveRoads)
            for step in route.steps {
                print(step.instructions)
            }
        }
    }
    
    func mapView(_ mapView: MKMapView, rendererFor
        overlay: MKOverlay) -> MKOverlayRenderer {
        
        let renderer = MKPolylineRenderer(overlay: overlay)
        renderer.strokeColor = UIColor(named: "PrimaryColor")
        renderer.lineWidth = 5.0
        return renderer
    }
    
    func centerMapOnLocation(location: CLLocation) {
        let coordinateRegion =
            MKCoordinateRegion(
                center: location.coordinate,
                latitudinalMeters: regionRadius,
                longitudinalMeters: regionRadius
        )
        directionsMap.setRegion(coordinateRegion, animated: true)
    }
}
