//
//  HttpClient.swift
//  ApiTest
//
//  Created by issd on 15/09/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import Foundation

class HttpClient {
    
    func get<T: Decodable>(url: URL,
                           ofType _: T.Type,
                           completion: @escaping (T?, URLResponse?, Error? ) -> ()) -> Void{
        handleRequest(method: "GET", url: url, ofType: T.self, completion: completion)
    }
    
    func post<T: Decodable>(url: URL,
                            body: Data,
                            ofType _: T.Type,
                            completion: @escaping (T?, URLResponse?, Error? ) -> ()) -> Void{
        handleRequest(method: "POST", url: url, body: body, ofType: T.self, completion: completion)
    }
    
    func put<T: Decodable>(url: URL,
                           body: Data,
                           ofType _: T.Type,
                           completion: @escaping (T?, URLResponse??, Error? ) -> ()) -> Void{
        handleRequest(method: "PUT", url: url, body: body, ofType: T.self, completion: completion)
    }
    
    func delete<T: Decodable>(url: URL,
                              ofType _: T.Type,
                              completion: @escaping (T?, URLResponse?, Error? ) -> ()) -> Void{
        handleRequest(method: "DELETE", url: url, ofType: T.self, completion: completion)
    }
    
    private func handleRequest<T: Decodable>(method: String,
                                             url: URL,
                                             body: Data? = nil,
                                             ofType _: T.Type,
                                             completion: @escaping (T?, URLResponse?, Error? ) ->()) -> Void{
        
        var request = URLRequest(url: url)
        request.httpMethod = method
        
        var headers = request.allHTTPHeaderFields ?? [:]
        headers["Content-Type"] = "application/json"
        
        // Check for token and add to header
//        let token = getProperty(withKey: "token") as? String
//        if (token != nil){
//            headers["Authorization"] = "Bearer " + token!
//        }
        
        request.allHTTPHeaderFields = headers
        
        if (method == "POST" || method == "PUT"){
            request.httpBody = body
        }
        
        let config = URLSessionConfiguration.default
        let session = URLSession(configuration: config)
        let task = session.dataTask(with: request) { (responseData, response, responseError) in
            DispatchQueue.main.async {
                if responseError != nil {
                    
                    completion(nil, response, responseError)
                } else {
                    let decoder = JSONDecoder()
                    
                    do {
                        let data = try decoder.decode(T.self, from: responseData!)
                        completion(data, response, responseError)
                    } catch{
                        print("Unexpected error: \(error).")
                        completion(nil, response, responseError)
                    }
                }
            }
        }
        task.resume()
    }
}
