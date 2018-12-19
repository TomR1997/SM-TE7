//
//  LaunchController.swift
//  Grocerly.iOS
//
//  Created by issd on 06/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import Foundation
import Lottie

class ShoppingListController : UIViewController, UITableViewDataSource{
    @IBOutlet weak var itemsText: UILabel!
    
    var animationView : LOTAnimationView!
    @IBOutlet weak var listTable: UITableView!
    
    var imageUtils = ImageUtils()
    
    var items = [Item]()
    var inCart = [Bool]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        listTable.dataSource = self
        DisplayLoader.instance.displayLoader(onView: view,name: "icon_loading")
        let loadedItems = UserPrefs().loadObject(forKey: "list_to_get", ofType: [Item].self)
        
        if let allItems = loadedItems{
            items = allItems
            listTable.reloadData()
            calculateItemsInCart()
        }
        
        let animationView = LOTAnimationView(name: "clock")
        animationView.loopAnimation = true
        animationView.contentMode = .scaleAspectFill
        animationView.center = view.center
        
        animationView.translatesAutoresizingMaskIntoConstraints = false
        view.addSubview(animationView)
        
        let verticalSpace = NSLayoutConstraint(item: self.listTable, attribute: .bottom, relatedBy: .equal, toItem: animationView, attribute: .top, multiplier: 1, constant: -4)
        NSLayoutConstraint.activate([
            verticalSpace,
            animationView.trailingAnchor.constraint(equalTo: view.trailingAnchor,constant: -16),
            animationView.widthAnchor.constraint(equalToConstant: 50),
            animationView.heightAnchor.constraint(equalToConstant: 50)
            ])
        
        animationView.play()
        
        DispatchQueue.main.asyncAfter(deadline: .now() + .seconds(2), execute: {
            DisplayLoader.instance.hideLoader()
        })
        
    }
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return items.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cellIdentifier = "listItem"
        guard let cell = tableView.dequeueReusableCell(withIdentifier: cellIdentifier, for: indexPath) as? ShoppingListCell else {
            fatalError("Could not cast cell to ShoppingListCell")
        }
        
        let item = items[indexPath.row]
        imageUtils.imageFromUrl(url: item.imageUrl) {targetImage in
            cell.itemView.image = targetImage
        }
        
        cell.ItemLabel.text = item.name
        
        cell.callback = { (myswitch) -> Void in
            self.inCart[indexPath.row] = myswitch.isOn
            self.calculateItemsInCart()
        }
        
        return cell
    }
    
    func calculateItemsInCart(){
        var count = 0;
        
        if (inCart.count == 0){
            for _ in 0..<items.count {
                inCart.append(false)
            }
        }
        
        if (items.count > 0){
            for i in 0..<items.count {
                if (!inCart[i]){
                count = count + 1
                }
            }
        }
        
        
        itemsText.text = String(count)
    }
    
    
}
