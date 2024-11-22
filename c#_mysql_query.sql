-- 데이터베이스 생성 (UTF-8 설정)
CREATE DATABASE ParkingManagementSystem CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 생성한 데이터베이스 사용
USE ParkingManagementSystem;

-- 주차 공간 테이블
CREATE TABLE ParkingSpot (
    spot_number INT PRIMARY KEY,               -- 주차 공간 번호, PK로 사용
    is_disabled BOOLEAN DEFAULT FALSE,         -- 장애인 전용 주차 공간 여부 (기본값: FALSE)
    is_occupied BOOLEAN DEFAULT FALSE,         -- 주차 공간 점유 여부 (기본값: FALSE)
    vehicle_number VARCHAR(10) UNIQUE          -- 차량 번호 (고유값으로 설정)
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 차량 테이블
CREATE TABLE Vehicle (
    vehicle_number VARCHAR(10) PRIMARY KEY,    -- 차량 번호를 기본 키로 설정
    vehicle_type ENUM('Standard', 'Compact') NOT NULL, -- 차량 타입 (일반차량, 경차)
    start_time TIMESTAMP,                      -- 입차 시간
    end_time TIMESTAMP NULL,                   -- 출차 시간, NULL 허용
    parking_fee INT DEFAULT 0                  -- 주차 요금 (기본값: 0)
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 영수증 테이블
CREATE TABLE Receipt (
    receipt_id INT AUTO_INCREMENT PRIMARY KEY, -- 영수증 고유 ID, 자동 증가
    vehicle_number VARCHAR(10) NOT NULL,       -- 차량 번호 (외래 키)
    parking_fee_before_discount INT NOT NULL,  -- 할인 전 주차 요금
    discount_amount INT DEFAULT 0,             -- 할인 금액
    total_fee INT NOT NULL,                    -- 최종 요금 (할인 후)
    parking_duration INT NOT NULL,             -- 주차 시간 (분 단위)
    start_time TIMESTAMP,                      -- 입차 시간
    end_time TIMESTAMP NULL,                   -- 출차 시간, NULL 허용
    FOREIGN KEY (vehicle_number) REFERENCES Vehicle(vehicle_number) -- 차량 번호 외래 키 연결
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 스토어 할인 테이블
CREATE TABLE StoreDiscount (
    store_name VARCHAR(50) PRIMARY KEY,        -- 스토어 이름, PK로 사용
    discount_percentage INT NOT NULL,          -- 할인율
    discount_condition INT NOT NULL,           -- 할인 적용 조건
    vehicle_number VARCHAR(10),                -- 차량 번호 (Vehicle 테이블과 연동)
    FOREIGN KEY (vehicle_number) REFERENCES Vehicle(vehicle_number) -- 외래 키 설정
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 주차 공간 초기화 데이터 삽입: 1~25번은 일반, 26~30번은 장애인 공간으로 설정
INSERT INTO ParkingSpot (spot_number, is_disabled)
VALUES
    (1, FALSE), (2, FALSE), (3, FALSE), (4, FALSE), (5, FALSE),
    (6, FALSE), (7, FALSE), (8, FALSE), (9, FALSE), (10, FALSE),
    (11, FALSE), (12, FALSE), (13, FALSE), (14, FALSE), (15, FALSE),
    (16, FALSE), (17, FALSE), (18, FALSE), (19, FALSE), (20, FALSE),
    (21, FALSE), (22, FALSE), (23, FALSE), (24, FALSE), (25, FALSE),
    (26, TRUE), (27, TRUE), (28, TRUE), (29, TRUE), (30, TRUE);

-- 스토어 할인 데이터 삽입
INSERT INTO StoreDiscount (store_name, discount_percentage, discount_condition) VALUES
    ('nike', 20, 50000),           -- 의류점 nike: 5만원 이상 결제 시 20% 할인
    ('빵빵이의 빵집', 10, 20000),    -- 빵가게 빵빵이의 빵집: 2만원 이상 결제 시 10% 할인
    ('모수', 30, 150000);           -- 음식점 모수: 15만원 이상 결제 시 30% 할인
